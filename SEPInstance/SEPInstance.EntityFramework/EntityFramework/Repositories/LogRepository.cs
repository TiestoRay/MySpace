using SEPInstance.IDao.IEntityRepository;
using Abp.Auditing;
using Abp.EntityFramework;
namespace SEPInstance.EntityFramework.Repositories
{
    /// <summary>
    /// 日志接口的实现
    /// </summary>
    public class LogRepository : SEPInstanceRepositoryBase<AuditLog, long>, ILogRepository
    {
        public LogRepository(IDbContextProvider<SEPInstanceDbContext> dbContextProvider)
            : base(dbContextProvider)
        { }

        
    }
}
