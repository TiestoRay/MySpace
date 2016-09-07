using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using ABPInstance.EntityFramework;

namespace ABPInstance.Migrator
{
    [DependsOn(typeof(ABPInstanceDataModule))]
    public class ABPInstanceMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<ABPInstanceDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}