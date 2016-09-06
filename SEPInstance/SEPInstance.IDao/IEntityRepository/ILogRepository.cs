using Abp.Auditing;

namespace SEPInstance.IDao.IEntityRepository
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogRepository:IBaseRepository<AuditLog,long>
    {
        
    }
}
