using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SEPInstance.Users;

namespace SEPInstance.Dto.InputDto.Sessions
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 登陆名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
