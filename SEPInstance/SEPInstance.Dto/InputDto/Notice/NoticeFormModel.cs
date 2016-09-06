using System;
using System.ComponentModel.DataAnnotations;

namespace SEPInstance.Dto.InputDto.Notice
{
    /// <summary>
    /// 发送消息提交表单模型
    /// </summary>
    public class NoticeFormModel
    {
        /// <summary>
        /// 用户Id列表
        /// </summary>
        [Required]
        public string UserIdList { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [Required]
        public string NoticeContent { get; set; }

    }
}
