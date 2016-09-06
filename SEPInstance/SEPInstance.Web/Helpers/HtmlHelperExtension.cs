using SEPInstance.Authorization;
using SEPInstance.Users;
using Abp.Authorization;
using System.Linq;
using System.Linq.Expressions;
using Abp.Dependency;
using SEPInstance.AppService;
using System.Text;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// 是否拥有某个权限
        /// </summary>
        /// <param name="helper">html</param>
        /// <param name="PermissionName">权限常量</param>
        /// <returns></returns>
        public static bool HasAuthorization(this HtmlHelper helper, string PermissionName)
        {
            var _iocManager = IocManager.Instance;
            var _permissionManager = _iocManager.IocContainer.Resolve<IPermissionManager>();
            //所有权限
            var all = _permissionManager.GetAllPermissions();
            var _permissionChecker = _iocManager.IocContainer.Resolve<PermissionChecker>();
            //判断权限是否存在以及用户是否拥有当前权限
            if (all.Any(m => m.Name == PermissionName) && _permissionChecker.IsGranted(PermissionName))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 对某些权限进行判断，是否全部拥有
        /// </summary>
        /// <param name="helper">html</param>
        /// <param name="PermissionNames">权限数组</param>
        /// <returns></returns>
        public static bool HasAuthorization(this HtmlHelper helper, string[] PermissionNames)
        {
            var _iocManager = IocManager.Instance;
            var _permissionManager = _iocManager.IocContainer.Resolve<IPermissionManager>();
            //所有权限
            var all = _permissionManager.GetAllPermissions();
            var _permissionChecker = _iocManager.IocContainer.Resolve<PermissionChecker>();

            if (all.Count(m => PermissionNames.Contains(m.Name)) > 0 && _permissionChecker.IsGranted(false, PermissionNames))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 页面展示面包屑
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString BreadCrumb(this HtmlHelper helper)
        {
            var _iocManager = IocManager.Instance;
            var _sessionAppService = _iocManager.IocContainer.Resolve<SessionAppService>();
            //从session中获取当前用户的面包屑
            var list = _sessionAppService.GetUserCrumb();
            StringBuilder sb = new StringBuilder();
            if (list.Count() > 0)
            {
                sb.Append("<ol class=\"breadcrumb\">");
                for (int i = 0; i < list.Count(); i++)
                {
                    if (list.Count() > 1)
                    {
                        if (i == list.Count() - 1)
                        {
                            sb.Append("<li class=\"active\">" + list[i].Name + "</li>");
                        }
                        else
                        {
                            sb.Append("<li><a href=\"" + list[i].Url + "\">" + (i == 0 ? "<i class=\"fa fa-dashboard\"></i>" : "") + list[i].Name + "</a></li>");
                        }
                    }
                    else
                    {
                        sb.Append("<li class=\"active\">" + (i == 0 ? "<i class=\"fa fa-dashboard\"></i>" : "") + list[i].Name + "</li>");
                    }
                }
                sb.Append("</ol>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}