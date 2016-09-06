using Abp.Authorization.Roles;
using Abp.AutoMapper;
using SEPInstance.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SEPInstance.Dto.InputDto.Roles
{
    /// <summary>
    /// 创建角色输入DTO类，映射到实体Role
    /// </summary>
    [AutoMapTo(typeof(Role))]
    public class CreateRoleInput
    {
        /// <summary>
        /// 角色显示的名称
        /// </summary>
        [Required]
        [StringLength(Role.MaxDisplayNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [StringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
