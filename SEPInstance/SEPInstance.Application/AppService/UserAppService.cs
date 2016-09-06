using Abp.Domain.Uow;
using Abp.Authorization;
using SEPInstance.IAppService;
using SEPInstance.Authorization;
using SEPInstance.IDao.IEntityRepository;
using System.Threading.Tasks;
using SEPInstance.Dto.InputDto.Users;
using SEPInstance.Dto.OutputDto.Users;
using SEPInstance.Dto.OutputDto.Sessions;
using SEPInstance.Dto.OutputDto.Roles;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.Generic;
using System.Linq;
using SEPInstance.Users;
using Microsoft.AspNet.Identity;


namespace SEPInstance.AppService
{
    /// <summary>
    /// 用户接口的实现
    /// </summary>     
    public class UserAppService : SEPInstanceAppServiceBase, IUserAppService
    {
        /// <summary>
        /// 用户仓储接口
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// 权限管理者接口
        /// </summary>
        private readonly IPermissionManager _permissionManager;

        /// <summary>
        /// 工作单元管理者接口
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 角色仓储接口
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// 用户应用服务构造方法
        /// </summary>
        /// <param name="userRepository">IUserRepository</param>
        /// <param name="permissionManager">IPermissionManager</param>
        /// <param name="unitOfWorkManager">IUnitOfWorkManager</param>
        /// <param name="roleRepository">IRoleRepository</param>
        public UserAppService(
            IUserRepository userRepository,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
            _unitOfWorkManager = unitOfWorkManager;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 用户列表分页
        /// </summary>
        /// <param name="input">UserListInput</param>
        /// <returns>UserPagedDto<UserListDto></returns>
        public UserPagedDto<UserListDto> GetUsersPagedList(UserListInput input)
        {
            var users = _userRepository.ToPagedList(m => string.IsNullOrEmpty(input.KeyWord) ? true : m.UserName.Contains(input.KeyWord), input.PageIndex ?? 0, 3);
            var resultDto = new UserPagedDto<UserListDto>(users);
            return resultDto;
        }

        /// <summary>
        /// 取出未删除并且TenantId==1的所有用户
        /// </summary>
        /// <returns>List<UserListDto></returns>
        public List<UserListDto> GetUsersList()
        {
            var list = _userRepository.GetAllList("SELECT * FROM AbpUsers WHERE IsDeleted = 0 AND Id!=1");
            return list.MapTo<List<UserListDto>>();
        }

        /// <summary>
        /// 获取用户的信息
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>EditUserInput</returns>
        public EditUserInput GetEditUserInfo(long id)
        {
            var user = _userRepository.Get(id);
            return user.MapTo<EditUserInput>();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input">EditUserInput</param>
        public void UpdateUser(EditUserInput input)
        {
            var user = _userRepository.Get(input.UserId);
            user.Name = input.Name;
            user.EmailAddress = input.Email;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">long</param>
        public void DeleteUser(long id)
        {
            var u = _userRepository.Get(id);
            var roles = UserManager.GetRoles(id);

            UserManager.RemoveFromRoles(id, roles.ToArray());
            UserManager.Delete(u);
        }

        /// <summary>
        /// 获取用户当前角色，及系统所有角色
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>UserRoleSetOutput</returns>
        public UserRoleSetOutput GetUserRoleSetList(long id)
        {
            var list = _roleRepository.GetAllList();
            var result = list.MapTo<List<RoleListDto>>();
            var roleNames = UserManager.GetRoles<User, long>(id);
            UserRoleSetOutput output = new UserRoleSetOutput { AllRoleList = result, UserRoleList = roleNames.ToList(), Id = id };
            return output;
        }

        /// <summary>
        /// 重新设置用户的角色
        /// </summary>
        /// <param name="input">UserRoleInput</param>
        /// <returns>Task</returns>
        public async Task ResetUserRole(UserRoleInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);

            await UserManager.SetRoles(user, input.RolesList.ToArray());
        }

        /// <summary>
        /// 用户名重复验证
        /// </summary>
        /// <param name="input">UserValidate</param>
        /// <returns>bool</returns>
        public bool ValidateUserName(UserValidate input)
        {
            if (!input.UserId.HasValue)
                input.UserId = 0;

            return _userRepository.Count(m => m.UserName == input.UserName && m.Id != input.UserId.Value) == 0;
        }

        /// <summary>
        /// 用户邮箱重复验证
        /// </summary>
        /// <param name="input">UserValidate</param>
        /// <returns>bool</returns>
        public bool ValidateUserEmail(UserValidate input)
        {
            if (!input.UserId.HasValue)
                input.UserId = 0;
            return _userRepository.Count(m => m.EmailAddress == input.EmailAddress && m.Id != input.UserId.Value) == 0;
        }

        /// <summary>
        /// 将所有用户信息放入字典id-name
        /// </summary>
        /// <returns>Dictionary<long, string></returns>
        public Dictionary<long, string> GetAllUsersDic()
        {
            var allUser = _userRepository.GetAllList().ToDictionary(m => m.Id, m => m.UserName);
            return allUser;
        }

    }
}
