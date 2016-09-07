using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace ABPInstance.EntityFramework.Repositories
{
    public abstract class ABPInstanceRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<ABPInstanceDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ABPInstanceRepositoryBase(IDbContextProvider<ABPInstanceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class ABPInstanceRepositoryBase<TEntity> : ABPInstanceRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected ABPInstanceRepositoryBase(IDbContextProvider<ABPInstanceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
