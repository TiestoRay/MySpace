using Abp.Authorization.Users;

namespace SEPInstance.IDao.IEntityRepository
{
    /// <summary>
    /// 用户-角色接口
    /// </summary>
    public interface IUserRoleRepository : IBaseRepository<UserRole, long>
    {
    }
}
