using Abp.Application.Services.Dto;
using System.Collections.Generic;
using SEPInstance.Dto.OutputDto.Roles;

namespace SEPInstance.Dto.OutputDto.Users
{
    /// <summary>
    /// 用户角色设置DTO
    /// </summary>
    public class UserRoleSetOutput : EntityDto<long>
    {
        /// <summary>
        /// 系统所有的角色
        /// </summary>
        public List<RoleListDto> AllRoleList { get; set; }

        /// <summary>
        /// 该用户下的角色
        /// </summary>
        public List<string> UserRoleList { get; set; }
    }
}
