using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace SEPInstance.Dto.OutputDto.Users
{
    /// <summary>
    /// 用户列表
    /// </summary>
    [AutoMapFrom(typeof(SEPInstance.Users.User))]
    public class UserListDto : EntityDto<long>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Surname { get; set; }
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string FullName { get; set; }
        [Display(Name = "邮箱")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public bool IsEmailConfirmed { get; set; }
        [Display(Name = "上次登录")]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public bool IsActive { get; set; }
        [Display(Name = "创建时间")]
        public DateTime CreationTime { get; set; }
    }
}
