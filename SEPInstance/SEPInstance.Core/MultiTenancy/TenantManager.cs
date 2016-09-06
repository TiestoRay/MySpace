using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using SEPInstance.Authorization.Roles;
using SEPInstance.Editions;
using SEPInstance.Users;

namespace SEPInstance.MultiTenancy
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