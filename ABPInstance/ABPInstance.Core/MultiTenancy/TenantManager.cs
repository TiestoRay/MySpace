using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using ABPInstance.Authorization.Roles;
using ABPInstance.Editions;
using ABPInstance.Users;

namespace ABPInstance.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore
            ) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore
            )
        {
        }
    }
}