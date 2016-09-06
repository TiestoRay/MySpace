using Webdiyer.WebControls.Mvc;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Auditing;
using Abp.Dependency;
using Abp.AutoMapper;
using System;
using System.Linq;
using System.Linq.Expressions;
using SEPInstance.IAppService;
using SEPInstance.Dto.OutputDto.Log;
using SEPInstance.Dto.InputDto.Log;
using SEPInstance.Dto.OutputDto.Pager;
using SEPInstance.IDao.IEntityRepository;

namespace SEPInstance.AppService
{
    /// <summary>
    /// 日志接口的实现
    /// </summary>
    public class LogAppService:ILogAppService,ITransientDependency
    {
        /// <summary>
        /// 注入日志仓储接口
        /// </summary>
        private readonly ILogRepository _auditLogRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="auditLogRepository">审计日志仓储接口</param>
        public LogAppService(ILogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        /// <summary>
        /// 审计日志列表分页
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public PagedList<LogListDto> GetLogPagedList(LogListInput input)
        {
            var all = _auditLogRepository.GetAll();
            if (input.BeginTime.HasValue)
                all = all.Where(m => m.ExecutionTime >= input.BeginTime.Value);
            if (input.EndTime.HasValue)
                all = all.Where(m => m.ExecutionTime <= input.EndTime.Value);
            if (!string.IsNullOrEmpty(input.KeyWord))
            {
                all = all.Where(m => m.Exception.Contains(input.KeyWord) || m.Parameters.Contains(input.KeyWord));
            }
            var query = _auditLogRepository.ToPagedList<DateTime>(all,m=>m.ExecutionTime,false,input.PageIndex??0);
            var result = new PagedListDto<AuditLog, LogListDto>(query);
            if (result.Success)
                return result.DestinationList;
            return null;
        }
    }
}
