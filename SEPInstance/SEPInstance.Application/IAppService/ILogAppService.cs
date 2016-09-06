using Abp.Application.Services;
using Webdiyer.WebControls.Mvc;
using SEPInstance.Dto.OutputDto.Log;
using SEPInstance.Dto.InputDto.Log;

namespace SEPInstance.IAppService
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogAppService : IApplicationService
    {
        /// <summary>
        /// 日志列表分页
        /// </summary>
        /// <param name="input">LogListInput</param>
        /// <returns>PagedList<LogListDto></returns>
        PagedList<LogListDto> GetLogPagedList(LogListInput input);
    }
}
