using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace SEPInstance.Dto.InputDto.Roles
{
    /// <summary>
    /// 角色分配权限输入
    /// </summary>
    public class UpdateRolePermissionsInput
    {
        /// <summary>
        /// 角色id
        /// </summary>
        [Range(1, int.MaxValue)]
        public int RoleId { get; set; }

        /// <summary>
        /// 对应的权限列表
        /// </summary>
        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}
