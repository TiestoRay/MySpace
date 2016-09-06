using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace SEPInstance.Dto.InputDto.Search
{
    /// <summary>
    /// 搜索dto
    /// </summary>
    public class SearchDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int? PageIndex { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
    }
}
