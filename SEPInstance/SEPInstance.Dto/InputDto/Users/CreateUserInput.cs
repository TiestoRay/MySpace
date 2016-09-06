using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace SEPInstance.Dto.InputDto.Users
{
    /// <summary>
    /// 创建用户输入
    /// </summary>
    [AutoMap(typeof(SEPInstance.Users.User))]
    public class CreateUserInput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [StringLength(SEPInstance.Users.User.MaxNameLength)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        [Display(Name = "邮箱")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(SEPInstance.Users.User.MaxPlainPasswordLength)]
        [DisableAuditing]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
    }
}
