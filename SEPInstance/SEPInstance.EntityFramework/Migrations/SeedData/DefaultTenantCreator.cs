using System.Linq;
using SEPInstance.EntityFramework;
using SEPInstance.MultiTenancy;

namespace SEPInstance.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly SEPInstanceDbContext _context;

        public DefaultTenantCreator(SEPInstanceDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
