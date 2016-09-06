using System.Web.Mvc;
using SEPInstance.Dto.OutputDto.Log;
using SEPInstance.Dto.InputDto.Log;
using SEPInstance.IAppService;
using Abp.Authorization;
using Abp.Web.Mvc.Authorization;
using SEPInstance.Authorization;
using SEPInstance.Users;
using SEPInstance.Web.Filters;
using Abp.Auditing;

namespace SEPInstance.Web.Controllers
{
    [AbpMvcAuthorize]
    public class LogController : SEPInstanceControllerBase
    {
        private readonly ILogAppService _logAppService;
        private readonly IUserAppService _userAppService;
        public LogController(ILogAppService logAppService, IUserAppService userAppService)
        {
            _logAppService = logAppService;
            _userAppService = userAppService;
        }

        [BreadCrumb("日志列表")]
        [DisableAuditing]
        public ActionResult Index(LogListInput input)
        {
            var result = _logAppService.GetLogPagedList(input);
            var users = _userAppService.GetAllUsersDic();
            ViewBag.UserDic = users;
            if (Request.IsAjaxRequest())
                return PartialView("_AjaxLogList", result);
            return View(result);
        }
    }
}