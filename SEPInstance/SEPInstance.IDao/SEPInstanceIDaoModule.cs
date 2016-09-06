using System.Reflection;
using Abp.Modules;

namespace SEPInstance
{
    public class SEPInstanceIDaoModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
