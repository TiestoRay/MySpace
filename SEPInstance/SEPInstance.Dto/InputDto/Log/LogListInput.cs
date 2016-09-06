using SEPInstance.Dto.InputDto.Search;
using System;

namespace SEPInstance.Dto.InputDto.Log
{
    /// <summary>
    /// 日志查询输入
    /// </summary>
    public class LogListInput : SearchDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
