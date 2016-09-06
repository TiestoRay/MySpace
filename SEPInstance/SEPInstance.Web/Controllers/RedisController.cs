using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEPInstance.IAppService;
using SEPInstance.Redis;
using Abp.Web.Models;
using SEPInstance.Web.Filters;

namespace SEPInstance.Web.Controllers
{
    public class RedisController : SEPInstanceControllerBase
    {
        private readonly IRedisAppService _redisAppService;
        public RedisController(IRedisAppService redisAppService)
        {
            _redisAppService = redisAppService;
        }

    }
}