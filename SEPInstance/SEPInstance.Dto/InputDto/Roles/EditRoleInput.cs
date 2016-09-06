using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SEPInstance.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEPInstance.Dto.InputDto.Roles
{
    /// <summary>
    /// 编辑角色输入
    /// </summary>
    [AutoMapTo(typeof(Role))]
    public class EditRoleInput :EntityDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [Display(Name = "角色名")]
        [StringLength(Role.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [StringLength(Role.MaxDescriptionLength)]
        [Display(Name = "描述")]
        public string Description { get; set; }
    }
}
