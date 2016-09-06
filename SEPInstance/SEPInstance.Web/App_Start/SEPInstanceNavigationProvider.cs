using Abp.Application.Navigation;
using Abp.Localization;
using SEPInstance.Authorization;

namespace SEPInstance.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class SEPInstanceNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PermissionNames.Pages_Home_Index,
                        L("首页"),
                        url: "/Home/Index",
                        icon: "fa fa-home",
                        requiresAuthentication: true
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        PermissionNames.Pages_Users,
                        L("用户管理"),
                        url: "/Users/UsersList",
                        icon: "fa fa-users",
                        requiredPermissionName: PermissionNames.Pages_Users
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PermissionNames.Pages_Roles,
                        L("角色管理"),
                        url: "/Roles/RolesList",
                        icon: "fa fa-share-alt",
                        requiresAuthentication: true,
                        requiredPermissionName: PermissionNames.Pages_Roles
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PermissionNames.Pages_Log,
                        L("日志"),
                        url: "/Log/Index",
                        icon: "fa fa-sticky-note-o",
                        requiresAuthentication: true
                        )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new FixedLocalizableString(name);
        }
    }
}
