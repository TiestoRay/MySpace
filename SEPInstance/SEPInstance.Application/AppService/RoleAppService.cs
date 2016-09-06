using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Webdiyer.WebControls.Mvc;
using Microsoft.AspNet.Identity;
using Abp.Authorization;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.Authorization.Users;
using Abp.Domain.Uow;
using SEPInstance.Authorization;
using SEPInstance.Dto.InputDto.Roles;
using SEPInstance.Dto.OutputDto.Roles;
using SEPInstance.Dto.OutputDto.Pager;
using SEPInstance.Authorization.Roles;
using SEPInstance.IAppService;
using SEPInstance.IDao.IEntityRepository;

namespace SEPInstance.AppService
{
    /// <summary>
    /// 角色接口的实现
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RoleAppService : SEPInstanceAppServiceBase, IRoleAppService
    {
        /// <summary>
        /// 角色管理者
        /// </summary>
        private readonly RoleManager _roleManager;

        /// <summary>
        /// 权限管理者接口
        /// </summary>
        private readonly IPermissionManager _permissionManager;

        /// <summary>
        /// 角色仓储接口
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// 用户角色仓储接口
        /// </summary>
        private readonly IUserRoleRepository _userRoleRepository;

        /// <summary>
        /// 角色应用服务构造方法
        /// </summary>
        /// <param name="roleManager">角色管理者</param>
        /// <param name="permissionManager">权限管理者接口</param>
        /// <param name="roleRepository">角色仓储接口</param>
        /// <param name="userRolesReposity">用户角色仓储接口</param>
        public RoleAppService(RoleManager roleManager, IPermissionManager permissionManager, IRoleRepository roleRepository, IUserRoleRepository userRolesReposity)
        {
            _roleManager = roleManager;
            _permissionManager = permissionManager;
            _roleRepository = roleRepository;
            _userRoleRepository = userRolesReposity;
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>Role实体</returns>
        public Role GetRole(int id)
        {
            Role model = new Role();
            model = _roleRepository.Get(id);
            return model;
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns>List<RoleListDto></returns>
        [AbpAuthorize]
        public List<RoleListDto> GetRoleList()
        {
            var list = _roleRepository.GetAllList();
            var result = list.MapTo<List<RoleListDto>>();
            return result;
        }

        /// <summary>
        /// 编辑角色权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateRolePermissions(UpdateRolePermissionsInput input)
        {
            var list = new List<string>();
            foreach (var item in input.GrantedPermissionNames)
            {
                var parent = PermissionNames.GetAuthorizationParentName(item);
                if (parent != null)
                {
                    foreach (string sub in parent)
                    {
                        if (!input.GrantedPermissionNames.Contains(sub) && !list.Contains(sub))
                        {
                            list.Add(sub);
                        }
                    }
                }
            }
            if (list.Count() > 0)
                input.GrantedPermissionNames.AddRange(list);
            var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
            var t = _permissionManager.GetAllPermissions();
            var grantedPermissions = _permissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissionNames.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }

        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="input">RoleListInput</param>
        /// <returns>PagedList<RoleListDto></returns>
        [UnitOfWork(IsDisabled = true)]
        public PagedList<RoleListDto> GetPagedRoleList(RoleListInput input)
        {
            var roleList = _roleRepository.ToPagedList(x => string.IsNullOrEmpty(input.KeyWord) ? true : x.Name.Contains(input.KeyWord), input.PageIndex ?? 0, 3);
            var list = new PagedListDto<Role, RoleListDto>(roleList);
            if (list.Success)
                return list.DestinationList;
            return null;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input">CreateRoleInput</param>
        public void CreateRole(CreateRoleInput input)
        {
            var role = input.MapTo<Role>();

            RoleManager.Create<Role, int>(role);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">int</param>
        [UnitOfWork(IsDisabled = true)]
        public void DeleteRole(int id)
        {
            _roleRepository.Delete(id);
        }

        /// <summary>
        /// 根据角色主键获取角色实体
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>EditRoleInput</returns>
        public EditRoleInput EditRoleById(int id)
        {
            var role = _roleRepository.FirstOrDefault(id);
            if (role == null)
            {
                return null;
            }
            else
            {
                var input = role.MapTo<EditRoleInput>();
                return input;
            }
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="input">EditRoleInput</param>
        public void EditRole(EditRoleInput input)
        {
            var role = _roleRepository.FirstOrDefault(input.Id);
            if (role != null)
            {
                role.DisplayName = input.DisplayName;
                role.Description = input.Description;
            }
        }

        /// <summary>
        /// 根据角色主键获取此角色下的用户数
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        public int GetUsersByRoleId(int id)
        {
            int count = _userRoleRepository.GetAllList(x => x.RoleId == id).ToList().Count;

            return count;
        }

        /// <summary>
        /// 获取角色的所有权限
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>Task<List<Permission>></returns>
        public async Task<List<Permission>> GetPermissionsByRole(int id)
        {
            var permissions = await _roleManager.GetGrantedPermissionsAsync(id);
            return permissions.ToList();
        }
    }
}
