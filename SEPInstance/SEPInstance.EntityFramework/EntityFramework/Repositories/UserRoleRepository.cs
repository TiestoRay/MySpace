using SEPInstance.IDao.IEntityRepository;
using Abp.EntityFramework;
using Abp.Authorization.Users;
namespace SEPInstance.EntityFramework.Repositories
{
    /// <summary>
    /// 用户-角色接口的实现
    /// </summary>
    public class UserRoleRepository : SEPInstanceRepositoryBase<UserRole, long>, IUserRoleRepository
    {
        public UserRoleRepository(IDbContextProvider<SEPInstanceDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        
    }
}
