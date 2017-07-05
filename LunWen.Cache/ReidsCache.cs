using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using LunWen.Infrastructure;

namespace LunWen.Cache
{
    public class ReidsCache : ICache
    {
        private static ConnectionMultiplexer _redis = null;
        private static object _locker = new object();

        public static ConnectionMultiplexer Manager
        {
            get
            {
                if (_redis == null)
                {
                    lock (_locker)
                    {
                        if (_redis != null) return _redis;

                        _redis = GetManager();
                        return _redis;
                    }
                }

                return _redis;
            }
        }

        private static ConnectionMultiplexer GetManager(string connectionString = null)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                //server1:6379,server2:6379
                //server1:6379
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["redisConn"].ToString();
            }

            return ConnectionMultiplexer.Connect(connectionString);
        }

        public void Remove(string key)
        {
            var db = Manager.GetDatabase();
            db.KeyDelete(key);
        }

        public void Store(string key, object value)
        {
            var db = Manager.GetDatabase();

            var s = JsonHelper.SerializeObject(value);
            db.StringSet(key, s);
        }

        public T Get<T>(string key) where T : class
        {

            var db = Manager.GetDatabase();
            if (db.KeyExists(key))
            {
                string str = db.StringGet(key);
                return JsonHelper.DeserializeJsonToObject<T>(str);
            }
            else
            {
                return null;
            }
        }
    }
}
