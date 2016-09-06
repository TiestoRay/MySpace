using Abp.Application.Services;
using Abp.Domain.Uow;
using SEPInstance.IAppService;
using Abp.Dependency;
using Abp.Runtime.Caching;
using Abp.Runtime.Caching.Configuration;
using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using Abp.Runtime.Session;
using System.Linq;
using SEPInstance.Func;
using System.Configuration;
using SEPInstance.MultiTenancy;
using SEPInstance.IDao.IEntityRepository;

namespace SEPInstance.AppService
{
    /// <summary>
    /// Cache应用服务类
    /// </summary>
    public class CacheAppService : SEPInstanceAppServiceBase, ICacheAppService
    {
        /// <summary>
        /// Cache管理者
        /// </summary>
        private ICacheManager _cacheManager;

        /// <summary>
        /// 租户仓储接口
        /// </summary>
        private readonly ITenantRepository _tenantRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="cacheManager">Cache管理者</param>
        /// <param name="tenantRepository">租户仓储接口</param>
        public CacheAppService(ICacheManager cacheManager, ITenantRepository tenantRepository)
        {
            _cacheManager = cacheManager;
            _tenantRepository = tenantRepository;
        }

        /// <summary>
        /// 缓存租户表所有连接字符串
        /// </summary>
        public void CacheAllConnectString()
        {
            var datas = _tenantRepository.GetAllList();
            foreach (var data in datas)
            {
                var id = data.Id;
                string conn = "";
                if (!string.IsNullOrEmpty(data.ConnectionString))
                {
                    conn = DesFunc.Decrypt(data.ConnectionString);
                }
                else
                    conn = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                _cacheManager.GetCache<string, string>("Tenant_" + id).Set("Tenant_" + id, conn);
            }
        }

    }
}
