using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace ABPInstance
{
    [DependsOn(typeof(ABPInstanceCoreModule), typeof(AbpAutoMapperModule))]
    public class ABPInstanceApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
