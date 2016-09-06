using SEPInstance.IDao.IEntityRepository;
using Abp.Dependency;
using SEPInstance.MultiTenancy;
using Abp.EntityFramework;

namespace SEPInstance.EntityFramework.Repositories
{
    /// <summary>
    /// 租户接口的实现
    /// </summary>
    public class TenantRepository : SEPInstanceRepositoryBase<Tenant>, ITenantRepository
    {
        public TenantRepository(IDbContextProvider<SEPInstanceDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }
    }
}
