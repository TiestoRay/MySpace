using Abp.Web.Models;
using SEPInstance.IAppService;
using SEPInstance.Web.Filters;
using SEPInstance.Web.Models;
using SEPInstance.Dto.InputDto.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEPInstance.Web.Controllers
{
    /// <summary>
    /// 消息通知控制器类
    /// </summary>
    public class NoticeController : SEPInstanceControllerBase
    {
        /// <summary>
        /// 消息通知应用服务接口
        /// </summary>
        public INotificationAppService _notificationAppService;

        /// <summary>
        /// 用户应用服务接口
        /// </summary>
        public IUserAppService _userAppService;

        /// <summary>
        /// 消息通知构造函数注入消息通知和用户应用服务
        /// </summary>
        /// <param name="notificationAppService"></param>
        /// <param name="userAppService"></param>
        public NoticeController(INotificationAppService notificationAppService, IUserAppService userAppService)
        {
            _notificationAppService = notificationAppService;
            _userAppService = userAppService;
        }

        /// <summary>
        /// 消息通知管理页面
        /// </summary>
        /// <returns></returns>
        [BreadCrumb("消息通知管理")]
        public ActionResult Index()
        {
            var list = _userAppService.GetUsersList();
            //所有未删除并且tenantid=1的用户列表
            ViewBag.UserList = list;
            //用户Id拼接的字符串
            ViewBag.IdList = string.Join(",", list.Select(x => x.Id).ToArray());
            return View();
        }

        /// <summary>
        /// 发送消息方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回结果</returns>
        [HttpPost]
        public JsonResult SendMessage(NoticeFormModel model)
        {
            string[] userIds = model.UserIdList.Split(',');
            _notificationAppService.PublishMessage(userIds, model.NoticeContent);
            return Json(new AjaxResponse { Success = true });
        }
    }
}