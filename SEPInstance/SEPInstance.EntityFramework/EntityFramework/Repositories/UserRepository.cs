using SEPInstance.IDao.IEntityRepository;
using SEPInstance.Users;
using Abp.EntityFramework;
namespace SEPInstance.EntityFramework.Repositories
{
    /// <summary>
    /// 用户接口的实现
    /// </summary>
    public class UserRepository : SEPInstanceRepositoryBase<User, long>, IUserRepository
    {
        public UserRepository(IDbContextProvider<SEPInstanceDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        
    }
}
