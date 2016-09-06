
namespace SEPInstance.BreadCrumb
{
    /// <summary>
    /// 面包屑模型
    /// </summary>
    public class BreadCrumbModel
    {
        /// <summary>
        /// 显示名称（唯一）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指向的url地址
        /// </summary>
        public string Url { get; set; }

    }
}
