using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace SEPInstance
{
    [DependsOn(typeof(SEPInstanceCoreModule), typeof(AbpAutoMapperModule), typeof(SEPInstanceDtoModule), typeof(SEPInstanceIDaoModule))]
    public class SEPInstanceApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
