using Abp.Application.Services;

namespace SEPInstance.IAppService
{
    /// <summary>
    /// Cache缓存接口
    /// </summary>
    public interface ICacheAppService:IApplicationService
    {
        /// <summary>
        /// 缓存租户表所有连接字符串
        /// </summary>
        void CacheAllConnectString();

    }
}
