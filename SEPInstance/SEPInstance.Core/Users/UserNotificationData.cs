using Abp.Notifications;

namespace SEPInstance.Users
{
    /// <summary>
    /// 自定义用户消息通知类
    /// </summary>
    public class UserNotificationData : NotificationData
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string FriendshipMessage { get; set; }

        public UserNotificationData(string friendshipMessage)
        {
            FriendshipMessage = friendshipMessage;
        }

    }
}
