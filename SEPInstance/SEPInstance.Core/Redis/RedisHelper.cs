using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SEPInstance.Redis
{
    /// <summary>
    /// Redis帮助类
    /// </summary>
    public class RedisHelper
    {
        private static string RedisHost = ConfigurationManager.AppSettings["RedisHost"].ToString();
        private static int RedisPort = int.Parse(ConfigurationManager.AppSettings["RedisPort"]);
        private static string RedisPass = ConfigurationManager.AppSettings["RedisPassword"].ToString();
        private static ConnectionMultiplexer _redis;

        #region redis登陆连接
        /// <summary>
        /// 获取RedisClient
        /// </summary>
        /// <returns></returns>
        private static IDatabase GetClient(int dbid = 0)
        {
            if (_redis == null)
            {
                ConfigurationOptions options = new ConfigurationOptions();
                options.EndPoints.Add(new DnsEndPoint(RedisHost, RedisPort));
                options.Password = RedisPass;
                options.ResolveDns = true;
                options.ClientName = "SEPInstance.Core";
                options.SyncTimeout = 60000;//操作超时时间为1分钟
                options.ConnectTimeout = 10000;
                _redis = ConnectionMultiplexer.Connect(options);
            }
            IDatabase db = _redis.GetDatabase(dbid);
            return db;
        }
        #endregion

        #region hash：key-value操作

        /// <summary>
        /// 新增key-value
        /// </summary>
        /// <typeparam name="T">泛型类继承RedisModel</typeparam>
        /// <param name="List">数据</param>
        /// <param name="Key">key</param>
        public static void SetHashValue<T>(List<T> List, string Key = SEPInstanceConsts.DefaultRedisKey) where T : RedisModel
        {
            var _db = GetClient();
            HashEntry[] entry = List.Select(m => new HashEntry(m.Id, JsonConvert.SerializeObject(m))).ToArray();
            _db.HashSet(Key, entry);
        }

        /// <summary>
        /// 获取hash列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="Ids">键的列表</param>
        /// <param name="Key">key</param>
        /// <returns></returns>
        public static List<T> GetHashValue<T>(List<int> Ids, string Key = SEPInstanceConsts.DefaultRedisKey)
        {
            var _db = GetClient();
            var Keys = Ids.Select(m => (RedisValue)m).ToArray();
            var list = _db.HashGet(Key, Keys);
            var result = list.Select(m => JsonConvert.DeserializeObject<T>(m)).ToList();
            return result;
        }

        /// <summary>
        /// 删除hash
        /// </summary>
        /// <param name="Ids">键的列表</param>
        /// <param name="Key">key</param>
        public static void DelHashValue(List<int> Ids, string Key = SEPInstanceConsts.DefaultRedisKey)
        {
            var _db = GetClient();
            var Keys = Ids.Select(m => (RedisValue)m).ToArray();
            _db.HashDelete(Key, Keys);
        }

        #endregion

        #region list链表操作
        /// <summary>
        /// 新增列表
        /// </summary>
        /// <param name="List">列表数据</param>
        /// <param name="Key">key</param>
        public static void SetList(List<RedisModel> List, string Key = SEPInstanceConsts.DefaultRedisKey)
        {
            var _db = GetClient();
            var result = List.Select(m => (RedisValue)JsonConvert.SerializeObject(m)).ToArray();
            _db.ListRightPush(Key, result);
        }

        /// <summary>
        /// 新增列表
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="List">列表数据</param>
        /// <param name="Key">key</param>
        public static void SetList<T>(List<T> List, string Key = SEPInstanceConsts.DefaultRedisKey)
        {
            var _db = GetClient();
            var result = List.Select(m => (RedisValue)JsonConvert.SerializeObject(m)).ToArray();
            _db.ListRightPush(Key, result);
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="Key">key</param>
        /// <param name="start">开始序号</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static List<RedisModel> GetList(string Key = SEPInstanceConsts.DefaultRedisKey, int start = 0, int length = 0)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, start, start + length - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<RedisModel>(m)).ToList();
            return result;
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">key</param>
        /// <param name="start">开始序号</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static List<T> GetList<T>(string Key = SEPInstanceConsts.DefaultRedisKey, int start = 0, int length = 0)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, start, start + length - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<T>(m)).ToList();
            return result;
        }

        /// <summary>
        /// 返回获取的列表数据，并从redis中删除（从0开始）
        /// </summary>
        /// <param name="Key">key</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static List<RedisModel> GetListThenDel(string Key = SEPInstanceConsts.DefaultRedisKey, int length = 0)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, 0, length - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<RedisModel>(m)).ToList();
            if (result.Count > 0)
            {
                _db.ListTrim(Key, length, -1);
            }
            return result;
        }

        /// <summary>
        /// 返回获取的列表数据，并从redis中删除（从0开始）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">key</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static List<T> GetListThenDel<T>(string Key = SEPInstanceConsts.DefaultRedisKey, int length = 0)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, 0, length - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<T>(m)).ToList();
            if (result.Count > 0)
            {
                _db.ListTrim(Key, length, -1);
            }
            return result;
        }

        /// <summary>
        /// 删除列表
        /// </summary>
        /// <param name="Key">key</param>
        /// <param name="length">长度</param>
        public static void DelList(string Key = SEPInstanceConsts.DefaultRedisKey, int length = 0)
        {
            var _db = GetClient();
            _db.ListTrim(Key, length, -1);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="Key">key</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<RedisModel> GetPagedList(string Key = SEPInstanceConsts.DefaultRedisKey, int PageIndex = 1, int PageSize = 10)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, (PageIndex - 1) * PageSize, PageIndex * PageSize - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<RedisModel>(m)).ToList();
            return result;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key">key</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<T> GetPagedList<T>(string Key = SEPInstanceConsts.DefaultRedisKey, int PageIndex = 1, int PageSize = 10)
        {
            var _db = GetClient();
            var list = _db.ListRange(Key, (PageIndex - 1) * PageSize, PageIndex * PageSize - 1);
            var result = list.Select(m => JsonConvert.DeserializeObject<T>(m)).ToList();
            return result;
        }

        #endregion

        #region string操作
        /// <summary>
        /// 将数据保存至Redis
        /// </summary>
        /// <param name="key">要保存的位置</param>
        /// <param name="vals">保存的值</param>
        public static void Save(string key, params string[] vals)
        {
            var client = GetClient();
            var values = vals.Select(s => (RedisValue)s).ToArray();
            var result = client.ListRightPush(key, values);
        }

        /// <summary>
        /// 读取数据所使用的Key
        /// </summary>
        /// <param name="key">读取数据所使用的Key</param>
        /// <returns>从Redis中读取到的数据</returns>
        public static string[] Load(string key)
        {
            var client = GetClient();
            var length = SEPInstanceConsts.MaxRowsPerLoad;
            var vals = client.ListRange(key, 0, length - 1);
            if (vals.Length > 0)
                client.ListTrim(key, vals.Length, -1);
            var result = new List<string>(vals.Length);
            foreach (var v in vals)
            {
                if (!v.IsNullOrEmpty)
                    result.Add(v);
            }
            return result.ToArray();
        }
        #endregion
    }
}
