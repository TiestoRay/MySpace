using Abp.Application.Services;
using System.Threading.Tasks;

namespace SEPInstance.IAppService
{
    /// <summary>
    /// 消息通知应用服务
    /// </summary>
    public interface INotificationAppService:IApplicationService
    {
        /// <summary>
        /// 发布通知
        /// </summary>
        /// <param name="tenantId">int? 租户id</param>
        /// <param name="message">string 消息</param>
        void PublishMessage(int? tenantId, string message);


        /// <summary>
        /// 给指定用户发布通知
        /// </summary>
        /// <param name="userIds">string[] 用户id数组</param>
        /// <param name="message">string 消息</param>
        void PublishMessage(string[] userIds, string message);

    }
}
