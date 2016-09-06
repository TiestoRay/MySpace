using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPInstance.Redis
{
    /// <summary>
    /// 所有key-value格式存入redis的父类
    /// </summary>
    public class RedisModel : RedisModel<int>
    {
    }

    /// <summary>
    /// 所有key-value格式存入redis的父类，key=(TPrimaryKey)Id,value=this
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public class RedisModel<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
