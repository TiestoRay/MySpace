using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using SEPInstance.EntityFramework;

namespace SEPInstance.Migrator
{
    [DependsOn(typeof(SEPInstanceDataModule))]
    public class SEPInstanceMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<SEPInstanceDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}