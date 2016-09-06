using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using SEPInstance.IAppService;
using SEPInstance.Web.Models.Layout;

namespace SEPInstance.Web.Controllers
{
    public class LayoutController : SEPInstanceControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;
        public readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        public LayoutController(
            IUserNavigationManager userNavigationManager,
            ISessionAppService sessionAppService,
            IMultiTenancyConfig multiTenancyConfig)
        {
            _userNavigationManager = userNavigationManager;
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
        }

        /// <summary>
        /// 系统导航菜单的展示
        /// </summary>
        /// <param name="activeMenu">当前活动的菜单</param>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult TopMenu(string activeMenu = "")
        {
            var model = new TopMenuViewModel
                        {
                            MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.ToUserIdentifier())),
                            ActiveMenuItemName = activeMenu
                        };

            return PartialView("_TopMenu", model);
        }

        /// <summary>
        /// 系统用户信息的展示
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult UserMenuOrLoginLink()
        {
            UserMenuOrLoginLinkViewModel model;

            if (AbpSession.UserId.HasValue)
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations()),
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                };
            }
            else
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled                    
                };
            }

            return PartialView("_UserMenuOrLoginLink", model);
        }
    }
}