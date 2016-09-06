using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;
using System.Linq.Expressions;
using Abp.Dependency;
using Abp.Domain.Repositories;

namespace SEPInstance.IDao.IEntityRepository
{
    /// <summary>
    /// 基础接口
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    public interface IBaseRepository<TEntity, TPrimaryKey>
         : IRepository,ITransientDependency where TEntity: class,Abp.Domain.Entities.IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">主键</param>
        void Delete(TPrimaryKey key);

        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        TEntity FirstOrDefault(TPrimaryKey key);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        TEntity Get(TPrimaryKey key);

        /// <summary>
        /// 根据sql获取
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        TEntity Get(string sql);

        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 数量
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        List<TEntity> GetAllList(string sql);

        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql">sql语句</param>
        void ExecuteCmd(string sql);

        /// <summary>
        /// 分页默认排序
        /// </summary>
        /// <param name="query">数据</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<TEntity> ToPagedList(IQueryable<TEntity> query, int pageIndex = 1, int pageSize = SEPInstanceConsts.PageSize);

        /// <summary>
        /// 分页排序
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="query">数据</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="ascending">是否升序，默认true</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<TEntity> ToPagedList<TKey>(IQueryable<TEntity> query, Expression<Func<TEntity, TKey>> orderLambda, bool ascending = true, int pageIndex = 1, int pageSize = SEPInstanceConsts.PageSize);

        /// <summary>
        /// 分页排序
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<TEntity> ToPagedList(Expression<Func<TEntity, bool>> whereLambda, int pageIndex = 1, int pageSize = SEPInstanceConsts.PageSize);

        /// <summary>
        /// 分页排序
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="ascending">是否升序，默认true</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<TEntity> ToPagedList<TKey>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> orderLambda, bool ascending = true, int pageIndex = 1, int pageSize = SEPInstanceConsts.PageSize);

        /// <summary>
        /// 分页排序
        /// </summary>
        /// <typeparam name="TKey1">排序字段类型1</typeparam>
        /// <typeparam name="TKey2">排序字段类型2</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda1">排序表达式1</param>
        /// <param name="orderLambda2">排序表达式2</param>
        /// <param name="ascending1">是否升序1，默认true</param>
        /// <param name="ascending2">是否升序2，默认true</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        PagedList<TEntity> ToPagedList<TKey1, TKey2>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey1>> orderLambda1, Expression<Func<TEntity, TKey2>> orderLambda2, bool ascending1 = true, bool ascending2 = true, int pageIndex = 1, int pageSize = SEPInstanceConsts.PageSize);
    }

    /// <summary>
    /// 基础接口
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, int>
        where TEntity : class,Abp.Domain.Entities.IEntity<int>
    {

    }
}
