using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using SEPInstance.BreadCrumb;

namespace SEPInstance.Dto.OutputDto.Sessions
{
    /// <summary>
    /// 用户扩展session类
    /// </summary>
    public class SessionStore : IOutputDto
    {
        /// <summary>
        /// 上次更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 用户登录信息
        /// </summary>
        public GetCurrentLoginInformationsOutput LoginInfo { get; set; }

        /// <summary>
        /// 面包屑信息
        /// </summary>
        public List<BreadCrumbModel> BreadCrumbInfo { get; set; }
    }
}
