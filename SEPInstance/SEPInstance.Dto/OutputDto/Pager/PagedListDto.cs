using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdiyer.WebControls.Mvc;
using Abp.AutoMapper;

namespace SEPInstance.Dto.OutputDto.Pager
{
    /// <summary>
    /// 分页转换DTO
    /// </summary>
    /// <typeparam name="TSource">转换前的数据</typeparam>
    /// <typeparam name="TDestination">转换后的数据</typeparam>
    public class PagedListDto<TSource, TDestination>
        where TSource : class
        where TDestination : class
    {
        /// <summary>
        /// 根据是否成功来判断获取dto数据
        /// </summary>
        public bool Success
        {
            get
            {
                try
                {
                    this.DestinationList = this.ToPagedList();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            set { }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public PagedListDto()
        {
            this.Success = false;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="SourceList">转换源列表</param>
        public PagedListDto(PagedList<TSource> SourceList)
        {
            this.SourceList = SourceList;
            this.Success = false;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="SourceList">转换源列表</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">总页数</param>
        /// <param name="totalItemCount">总记录数</param>
        public PagedListDto(List<TSource> SourceList, int pageIndex, int pageSize, int totalItemCount)
        {
            this.SourceList = new PagedList<TSource>(SourceList, pageIndex, pageSize, totalItemCount);
            this.Success = false;
        }

        /// <summary>
        /// 实体分页数据
        /// </summary>
        public PagedList<TSource> SourceList { get; set; }

        /// <summary>
        /// DTO转换后的数据
        /// </summary>
        public PagedList<TDestination> DestinationList
        {
            get;
            set;
        }

        /// <summary>
        /// DTO转换
        /// </summary>
        /// <returns></returns>
        public PagedList<TDestination> ToPagedList()
        {
            var list = this.SourceList;
            var temp = list.Select(m => m.MapTo<TDestination>());
            return new PagedList<TDestination>(temp, list.CurrentPageIndex, list.PageSize, list.TotalItemCount);
        }

    }
}
