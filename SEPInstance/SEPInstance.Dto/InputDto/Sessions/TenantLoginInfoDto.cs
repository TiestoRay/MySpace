using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using SEPInstance.MultiTenancy;

namespace SEPInstance.Dto.InputDto.Sessions
{
    /// <summary>
    /// 租户登陆信息
    /// </summary>
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        /// <summary>
        /// 租户名
        /// </summary>
        public string TenancyName { get; set; }

        /// <summary>
        /// 租户显示名称
        /// </summary>
        public string Name { get; set; }
    }
}
