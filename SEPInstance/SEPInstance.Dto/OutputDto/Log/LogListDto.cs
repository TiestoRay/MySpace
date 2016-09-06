using Abp.Application.Services.Dto;
using System;
using Abp.AutoMapper;
using Abp.Auditing;
using System.ComponentModel.DataAnnotations;

namespace SEPInstance.Dto.OutputDto.Log
{
    /// <summary>
    /// 日志列表输出
    /// </summary>
    [AutoMapFrom(typeof(AuditLog))]
    public class LogListDto : EntityDto<long>
    {
        /// <summary>
        /// 租户id
        /// </summary>
        [Display(Name = "租户id")]
        public int? TenantId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Display(Name = "用户id")]
        public long? UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 服务类
        /// </summary>
        [Display(Name = "服务类")]
        public string ServiceName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        [Display(Name = "方法")]
        public string MethodName { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [Display(Name = "参数")]
        public string Parameters { get; set; }

        /// <summary>
        /// 开始执行时间
        /// </summary>
        [Display(Name = "开始执行时间")]
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 整个方法执行周期
        /// </summary>
        [Display(Name = "执行时间")]
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// 客户端ip
        /// </summary>
        [Display(Name = "客户端IP")]
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        [Display(Name = "客户端名称")]
        public string ClientName { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [Display(Name = "浏览器信息")]
        public virtual string BrowserInfo { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [Display(Name = "异常信息")]
        public virtual string Exception { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [Display(Name = "用户")]
        public long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [Display(Name = "租户Id")]
        public int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// 自定义信息
        /// </summary>
        [Display(Name = "自定义信息")]
        public string CustomData { get; set; }
    }
}
