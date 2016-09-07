using ABPInstance.EntityFramework;
using EntityFramework.DynamicFilters;

namespace ABPInstance.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly ABPInstanceDbContext _context;

        public InitialHostDbBuilder(ABPInstanceDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
