using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEPInstance.IAppService;
using SEPInstance.BreadCrumb;
using Abp.Domain.Repositories;
using Abp.Dependency;
using SEPInstance.AppService;
using System.Security.Claims;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace SEPInstance.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple=false)]
    public class BreadCrumbAttribute : ActionFilterAttribute, ITransientDependency
    {
        /// <summary>
        /// 面包屑显示名称
        /// </summary>
        public string Name { get; set; }
         
        /// <summary>
        /// 用户session接口
        /// </summary>
        private ISessionAppService _sessionAppService
        {
            get
            {
                return IocManager.Instance.Resolve<SessionAppService>();
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BreadCrumbAttribute()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Name"></param>
        public BreadCrumbAttribute(string Name)
        {
            this.Name = Name;
        }
        /// <summary>
        /// action过滤
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                //非Ajax请求
                if (filterContext.HttpContext.Request.HttpMethod.ToLower()=="get" && !filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    string url = filterContext.HttpContext.Request.Url.ToString();
                    var identity=filterContext.HttpContext.User.Identity as ClaimsIdentity;
                    _sessionAppService.PushToUserCrumb(new BreadCrumbModel { Name = this.Name, Url = url }, identity);
                    filterContext.HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    filterContext.HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
                }
            }
            base.OnResultExecuting(filterContext);
        }
    }
}