namespace SEPInstance
{
    public class SEPInstanceConsts
    {
        public const string LocalizationSourceName = "SEPInstance";

        /// <summary>
        /// 默认每页大小
        /// </summary>
        public const int PageSize = 10;

        #region redis

        /// <summary>
        /// 默认redis键
        /// </summary>
        public const string DefaultRedisKey = "Default";

        /// <summary>
        /// 每次读取最大条数
        /// </summary>
        public const int MaxRowsPerLoad = 1000;

        /// <summary>
        /// 订阅的名称
        /// </summary>
        public const string SubscriptionName = "Subscription_Name";

        /// <summary>
        /// 扩展session的类型
        /// </summary>
        public const string SessionExtensionClaimType = "http://www.aspnetboilerplate.com/identity/claims/SessionExtension";

        #endregion
    }
}