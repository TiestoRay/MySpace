using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Zero.Configuration;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using SEPInstance.Api;
using Hangfire;
using SEPInstance.AppService;

namespace SEPInstance.Web
{
    [DependsOn(
        typeof(SEPInstanceDataModule),
        typeof(SEPInstanceApplicationModule),
        typeof(SEPInstanceWebApiModule),
        typeof(AbpWebSignalRModule),
        //typeof(AbpHangfireModule), - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
        typeof(AbpWebMvcModule))]
    public class SEPInstanceWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //本地化默认开启
            Configuration.Localization.IsEnabled = false;

            ////配置导航菜单
            Configuration.Navigation.Providers.Add<SEPInstanceNavigationProvider>();

            //模块审计日志，默认开启
            Configuration.Auditing.IsEnabled = true;
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void PostInitialize()
        {
            //缓存租户所有链接字符串
            var _cacheAppService = IocManager.IocContainer.Resolve<CacheAppService>();
            _cacheAppService.CacheAllConnectString();
            base.PostInitialize();
        }
    }
}
