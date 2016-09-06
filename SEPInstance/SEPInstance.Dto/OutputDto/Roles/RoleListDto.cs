using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SEPInstance.Authorization.Roles;
using SEPInstance.Users;

namespace SEPInstance.Dto.OutputDto.Roles
{
    /// <summary>
    /// 角色输出DTO类，从实体Role进行映射
    /// </summary>
    [AutoMapFrom(typeof(Role))]
    public class RoleListDto : EntityDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 是否已经删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人id
        /// </summary>
        public int? DeleterUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 上次修改人
        /// </summary>
        public string LastModifierUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserName { get; set; }

        

    }
}
