using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Webdiyer.WebControls.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SEPInstance.EntityFramework.Repositories
{
    public abstract class SEPInstanceRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<SEPInstanceDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContextProvider"></param>
        protected SEPInstanceRepositoryBase(IDbContextProvider<SEPInstanceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Add(TEntity entity)
        {
            return base.Insert(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public TEntity Update(TEntity entity) {
            return base.Update(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        public void Delete(TEntity entity) {
            base.Delete(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">主键</param>
        public void Delete(TPrimaryKey key) {
            base.Delete(key);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public TEntity Get(TPrimaryKey key) {
            return base.Get(key);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public TEntity Get(string sql) {
            var Result = Context.Database.SqlQuery<TEntity>(sql).FirstOrDefault();
            return Result;
        }

        /// <summary>
        /// 获取第一条
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public TEntity FirstOrDefault(TPrimaryKey key)
        {
            return base.FirstOrDefault(key);
        }

        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return base.Count();
        }

        /// <summary>
        /// 数量
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return base.Count(where);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetAll() {
            return base.GetAll();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAllList()
        {
            return base.GetAllList();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return base.GetAllList(predicate);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public List<TEntity> GetAllList(string sql)
        {
            return Context.Database.SqlQuery<TEntity>(sql,new SqlParameter[]{}).ToList();
        }

        /// <summary>
        /// 执行sql命令
        /// </summary>
        /// <param name="sql">sql语句</param>
        public void ExecuteCmd(string sql)
        {
            Context.Database.ExecuteSqlCommand(sql, new SqlParameter[] { });
        }
        #region 自定义函数

        /// <summary>
        /// 分页默认排序
        /// </summary>
        /// <param name="query">数据</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PagedList<TEntity> ToPagedList(IQueryable<TEntity> query, int pageIndex, int pageSize) {
            return query.OrderBy(m => m.Id).ToPagedList(pageIndex, pageSize);
        }

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
        public PagedList<TEntity> ToPagedList<TKey>(IQueryable<TEntity> query, Expression<Func<TEntity, TKey>> orderLambda, bool ascending, int pageIndex, int pageSize)
        {
            if (ascending)
                return query.OrderBy(orderLambda).ToPagedList(pageIndex, pageSize);
            else
                return query.OrderByDescending(orderLambda).ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页排序
        /// </summary>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public PagedList<TEntity> ToPagedList(Expression<Func<TEntity, bool>> whereLambda, int pageIndex, int pageSize)
        {
            return Context.Set<TEntity>().Where<TEntity>(whereLambda).OrderBy(m => m.Id).ToPagedList(pageIndex, pageSize);
        }

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
        public PagedList<TEntity> ToPagedList<TKey>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey>> orderLambda, bool ascending, int pageIndex, int pageSize)
        {
            if (ascending)
                return Context.Set<TEntity>().Where<TEntity>(whereLambda).OrderBy(orderLambda).ToPagedList(pageIndex, pageSize);
            else
                return Context.Set<TEntity>().Where<TEntity>(whereLambda).OrderByDescending(orderLambda).ToPagedList(pageIndex, pageSize);
        }

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
        public PagedList<TEntity> ToPagedList<TKey1, TKey2>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TKey1>> orderLambda1, Expression<Func<TEntity, TKey2>> orderLambda2, bool ascending1, bool ascending2, int pageIndex, int pageSize)
        {
            if (ascending1)
            {
                if (ascending2)
                {
                    return Context.Set<TEntity>().Where<TEntity>(whereLambda).OrderBy(orderLambda1).OrderBy(orderLambda2).ToPagedList(pageIndex, pageSize);
                }
                else
                {
                    return Context.Set<TEntity>().Where<TEntity>(whereLambda).OrderBy(orderLambda1).OrderByDescending(orderLambda2).ToPagedList(pageIndex, pageSize);
                }
            }
            else
            {
                if (ascending2)
                {
                    return Context.Set<TEntity>().Where<TEntity>(whereLambda).OrderByDescending(orderLambda1).OrderBy(orderLambda2).ToPagedList(pageIndex, pageSize);
                }
                else
                {
                    return Context.Set<TEntity>().Where<TEntity>(whereLambda).OrderByDescending(orderLambda1).OrderByDescending(orderLambda2).ToPagedList(pageIndex, pageSize);
                }
            }

        }
        #endregion
    }

    public abstract class SEPInstanceRepositoryBase<TEntity> : SEPInstanceRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected SEPInstanceRepositoryBase(IDbContextProvider<SEPInstanceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

    }
}
