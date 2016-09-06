﻿using SEPInstance.EntityFramework;
using EntityFramework.DynamicFilters;

namespace SEPInstance.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly SEPInstanceDbContext _context;

        public InitialHostDbBuilder(SEPInstanceDbContext context)
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
