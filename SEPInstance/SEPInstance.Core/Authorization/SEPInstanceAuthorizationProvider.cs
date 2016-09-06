using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace SEPInstance.Authorization
{
    public class SEPInstanceAuthorizationProvider : AuthorizationProvider
    {
        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="context">权限定义上下文</param>
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //初始化所有权限
            foreach (var permission in PermissionNames.GetAuthorization().Values)
            {
                var pages = context.CreatePermission(permission, L(PermissionNames.GetAuthorizationDisplayName(permission)));
            }
        }

        /// <summary>
        /// 本地化
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        private static ILocalizableString L(string name)
        {
            return new FixedLocalizableString(name);
        }
    }
}
