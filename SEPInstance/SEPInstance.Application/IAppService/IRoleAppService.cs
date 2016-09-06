using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Webdiyer.WebControls.Mvc;
using System.Collections.Generic;
using Abp.Authorization;
using SEPInstance.Dto.OutputDto.Roles;
using SEPInstance.Dto.InputDto.Roles;
using SEPInstance.Authorization.Roles;

namespace SEPInstance.IAppService
{
    /// <summary>
    /// 角色接口
    /// </summary>
    public interface IRoleAppService : IApplicationService
    {

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>Role实体</returns>
        Role GetRole(int id);

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns>List<RoleListDto></returns>
        List<RoleListDto> GetRoleList();

        /// <summary>
        /// 编辑角色权限
        /// </summary>
        /// <param name="input">编辑角色权限输入</param>
        /// <returns>Task</returns>
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);

        /// <summary>
        /// 根据查询条件获取角色分页记录
        /// </summary>
        /// <param name="input">角色分页查询条件</param>
        /// <returns>PagedList<RoleListDto></returns>
        PagedList<RoleListDto> GetPagedRoleList(RoleListInput input);

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input">创建角色输入</param>
        void CreateRole(CreateRoleInput input);

        /// <summary>
        /// 删除角色记录
        /// </summary>
        /// <param name="id">角色主键</param>
        void DeleteRole(int id);

        /// <summary>
        /// 根据角色主键获取角色记录
        /// </summary>
        /// <param name="id">角色主键</param>
        /// <returns>EditRoleInput</returns>
        EditRoleInput EditRoleById(int id);

        /// <summary>
        /// 编辑角色更新
        /// </summary>
        /// <param name="input">编辑角色输入</param>
        void EditRole(EditRoleInput input);

        /// <summary>
        /// 根据角色主键获取角色用户关联表中的用户个数
        /// </summary>
        /// <param name="id">角色主键</param>
        /// <returns></returns>
        int GetUsersByRoleId(int id);

        /// <summary>
        /// 根据角色获取对应的所有权限
        /// </summary>
        /// <param name="id">角色主键</param>
        /// <returns>权限列表</returns>
        Task<List<Permission>> GetPermissionsByRole(int id);
    }
}
