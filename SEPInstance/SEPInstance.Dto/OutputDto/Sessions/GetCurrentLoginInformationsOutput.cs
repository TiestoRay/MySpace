using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using SEPInstance.Dto.InputDto.Sessions;

namespace SEPInstance.Dto.OutputDto.Sessions
{
    /// <summary>
    /// 用户信息类输出
    /// </summary>
    public class GetCurrentLoginInformationsOutput : IOutputDto
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserLoginInfoDto User { get; set; }

        /// <summary>
        /// 租户信息
        /// </summary>
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
