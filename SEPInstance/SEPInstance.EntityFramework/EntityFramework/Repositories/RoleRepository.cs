using SEPInstance.IDao.IEntityRepository;
using SEPInstance.Authorization.Roles;
using Abp.EntityFramework;
namespace SEPInstance.EntityFramework.Repositories
{
    /// <summary>
    /// 角色接口的实现
    /// </summary>
    public class RoleRepository : SEPInstanceRepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IDbContextProvider<SEPInstanceDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        
    }
}
