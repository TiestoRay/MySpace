using Abp.Application.Services;
using SEPInstance.Dto.OutputDto.Users;
using SEPInstance.Dto.OutputDto.Sessions;
using SEPInstance.Dto.InputDto.Users;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SEPInstance.IAppService
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IUserAppService:IApplicationService
    {
        /// <summary>
        /// 用户列表分页
        /// </summary>
        /// <param name="input">用户查询输入</param>
        /// <returns>用户分页列表</returns>
        UserPagedDto<UserListDto> GetUsersPagedList(UserListInput input);

        /// <summary>
        /// 获取所有未删除并且tenantId==1的用户列表
        /// </summary>
        /// <returns>用户分页列表</returns>
        List<UserListDto> GetUsersList();

        /// <summary>
        /// 获取用户的信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>用户的信息</returns>
        EditUserInput GetEditUserInfo(long id);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input">修改后的用户信息</param>
        void UpdateUser(EditUserInput input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        void DeleteUser(long id);

        /// <summary>
        /// 重新设置用户的角色
        /// </summary>
        /// <param name="input">用户当前的角色</param>
        /// <returns>Task</returns>
        Task ResetUserRole(UserRoleInput input);

        /// <summary>
        /// 用户名重复验证
        /// </summary>
        /// <param name="input">用户id及用户名</param>
        /// <returns>true通过验证，false未通过</returns>
        bool ValidateUserName(UserValidate input);

        /// <summary>
        /// 用户邮箱重复验证
        /// </summary>
        /// <param name="input">用户id及用户邮箱</param>
        /// <returns>true通过验证，false未通过</returns>
        bool ValidateUserEmail(UserValidate input);

        /// <summary>
        /// 将所有用户信息放入字典id-name
        /// </summary>
        /// <returns>key-用户id，value-用户名</returns>
        Dictionary<long, string> GetAllUsersDic();

        /// <summary>
        /// 获取用户当前角色，及系统所有角色
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>用户角色</returns>
        UserRoleSetOutput GetUserRoleSetList(long id);

    }
}
