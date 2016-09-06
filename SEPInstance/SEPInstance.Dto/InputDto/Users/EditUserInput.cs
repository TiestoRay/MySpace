using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using AutoMapper;
using Abp.AutoMapper;
using SEPInstance.Users;

namespace SEPInstance.Dto.InputDto.Users
{
    /// <summary>
    /// 编辑用户输入
    /// </summary>
    public class EditUserInput
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [StringLength(SEPInstance.Users.User.MaxNameLength)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [Required]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        [EmailAddress]
        public string Email { get; set; }
    }
}