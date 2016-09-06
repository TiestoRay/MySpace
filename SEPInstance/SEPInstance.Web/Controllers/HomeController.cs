using System;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Web.Mvc.Authorization;
using SEPInstance.IAppService;
using SEPInstance.Authorization;
using SEPInstance.Users;
using Abp.Authorization;
using SEPInstance.Web.Filters;

namespace SEPInstance.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SEPInstanceControllerBase
    {
        private readonly INotificationAppService _notificationAppService;
        public HomeController(INotificationAppService notificationAppService)
        {
            _notificationAppService = notificationAppService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [BreadCrumb(Name = "首页")]
        public ActionResult Index()
        {
            _notificationAppService.PublishMessage(1, "欢迎使用SEPInstance");
            return View();
        }
	}
}