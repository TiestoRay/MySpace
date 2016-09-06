using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using SEPInstance.EntityFramework;

namespace SEPInstance
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(SEPInstanceCoreModule), typeof(SEPInstanceIDaoModule))]
    public class SEPInstanceDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SEPInstanceDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
