using System.Collections.Generic;
using SEPInstance.IAppService;
using Abp.Dependency;
using SEPInstance.Redis;

namespace SEPInstance.AppService
{
    /// <summary>
    /// Redis应用服务类
    /// </summary>
    public class RedisAppService : IRedisAppService, ITransientDependency
    {

    }
}
