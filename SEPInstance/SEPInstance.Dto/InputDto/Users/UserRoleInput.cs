using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEPInstance.Dto.InputDto.Users
{
    /// <summary>
    /// 用户分配角色输入
    /// </summary>
    public class UserRoleInput
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 角色名称列表
        /// </summary>
        public List<string> RolesList { get; set; }
    }
}