using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using SEPInstance.Authorization;
using SEPInstance.Authorization.Roles;
using SEPInstance.MultiTenancy;
using SEPInstance.Users;

namespace SEPInstance
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class SEPInstanceCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            //审计日志记录未登录用户操作
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //配置实体类型
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            //开启多租户
            Configuration.MultiTenancy.IsEnabled = true;

            //本地化关闭
            Configuration.Localization.IsEnabled = false;

            //配置角色管理
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);
            //加载权限
            Configuration.Authorization.Providers.Add<SEPInstanceAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
