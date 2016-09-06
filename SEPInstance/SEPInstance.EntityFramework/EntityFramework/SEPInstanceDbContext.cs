using System.Data.Common;
using Abp.Zero.EntityFramework;
using SEPInstance.Authorization.Roles;
using SEPInstance.MultiTenancy;
using SEPInstance.Users;

namespace SEPInstance.EntityFramework
{
    public class SEPInstanceDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public SEPInstanceDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in SEPInstanceDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of SEPInstanceDbContext since ABP automatically handles it.
         */
        public SEPInstanceDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public SEPInstanceDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
