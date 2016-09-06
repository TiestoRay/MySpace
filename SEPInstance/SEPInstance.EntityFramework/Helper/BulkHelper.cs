using System;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;

namespace SEPInstance.Helper
{
    /// <summary>
    /// EF批量操作类
    /// </summary>
    public static class BulkHelper
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="where">删除条件</param>
        /// <returns></returns>
        public static int BulkDelete<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            return source.Where<TEntity>(where).Delete<TEntity>();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="where">更新前的数据</param>
        /// <param name="whereNew">更改后的数据</param>
        /// <returns></returns>
        public static int BulkUpdate<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TEntity>> whereNew) where TEntity : class
        {
            return source.Where(where).Update<TEntity>(whereNew);
        }
    }
}
