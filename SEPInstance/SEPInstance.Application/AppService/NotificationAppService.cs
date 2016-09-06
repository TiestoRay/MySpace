using SEPInstance.IAppService;
using Abp.Dependency;
using Abp.Notifications;
using Abp.Runtime.Session;
using System.Threading.Tasks;
using System.Linq;
using Abp;
using SEPInstance.IDao.IEntityRepository;
using SEPInstance.Users;
using Abp.Domain.Uow;

namespace SEPInstance.AppService
{
    /// <summary>
    /// 消息通知应用服务
    /// </summary>
    public class NotificationAppService : INotificationAppService, ITransientDependency
    {
        #region
        /// <summary>
        /// 订阅通知管理者接口
        /// </summary>
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

        /// <summary>
        /// 发布通知管理者接口
        /// </summary>
        private readonly INotificationPublisher _notificationPublisher;

        /// <summary>
        /// 用户通知管理者
        /// </summary>
        private readonly IUserNotificationManager _userNotificationManager;

        /// <summary>
        /// 实时通知接口
        /// </summary>
        private readonly IRealTimeNotifier _realTimeNotifier;

        /// <summary>
        /// 用户仓储接口
        /// </summary>
        private readonly IUserRepository _userRepository;
        #endregion

        /// <summary>
        /// 通知应用服务构造方法
        /// </summary>
        /// <param name="notificationSubscriptionManager">订阅通知管理者接口</param>
        /// <param name="notificationPublisher">发布通知管理者接口</param>
        /// <param name="userNotificationManager">用户通知管理者</param>
        /// <param name="realTimeNotifier">实时通知接口</param>
        /// <param name="userRepository">用户仓储接口</param>
        public NotificationAppService(INotificationSubscriptionManager notificationSubscriptionManager, INotificationPublisher notificationPublisher, IUserNotificationManager userNotificationManager, IRealTimeNotifier realTimeNotifier, IUserRepository userRepository)
        {
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _notificationPublisher = notificationPublisher;
            _userNotificationManager = userNotificationManager;
            _realTimeNotifier = realTimeNotifier;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 租户订阅一般通知
        /// </summary>
        /// <param name="tenantId">租户</param>
        /// <returns></returns>
        private void Subscription_All(int? tenantId)
        {
            if (tenantId.HasValue)
            {
                var users = _userRepository.GetAllList();
                foreach (var user in users)
                {
                    if (_notificationSubscriptionManager.IsSubscribed(new UserIdentifier(tenantId.Value, user.Id), SEPInstanceConsts.SubscriptionName))
                        continue;
                    _notificationSubscriptionManager.Subscribe(new UserIdentifier(tenantId.Value, user.Id), SEPInstanceConsts.SubscriptionName);
                }
            }

        }

        /// <summary>
        /// 给当前租户下的所有用户发布通知
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <param name="message">消息</param>
        public void PublishMessage(int? tenantId, string message)
        {
            Subscription_All(tenantId);
            var data = new UserNotificationData(message);
            var users = _userRepository.GetAllList().Select(m => new UserIdentifier(m.TenantId, m.Id)).ToArray();
            _notificationPublisher.Publish(SEPInstanceConsts.SubscriptionName, data, userIds: users);
        }

        /// <summary>
        /// 给指定的用户发布通知
        /// </summary>
        /// <param name="userIds">用户id数组</param>
        /// <param name="message">消息</param>
        public void PublishMessage(string[] userIds, string message)
        {
            Subscription_All(null);
            var data = new UserNotificationData(message);
            var users = _userRepository.GetAllList().Where(x => userIds.Contains(x.Id.ToString())).Select(m => new UserIdentifier(m.TenantId, m.Id)).ToArray();
            _notificationPublisher.Publish(SEPInstanceConsts.SubscriptionName, data, userIds: users);
        }
    }
}
