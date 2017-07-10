using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Cache
{
    public class CacheManager
    {
        private static ICache _cache = null;
        private static object _locker = new object();

        public static ICache Cache
        {
            get
            {
                if (_cache == null)
                {
                    lock (_locker)
                    {
                        if (_cache != null) return _cache;

                        if (ConfigurationManager.AppSettings["CacheEnabled"] == "true")
                            _cache = new ReidsCache();
                        else
                            _cache = new NullCache();

                        return _cache;
                    }
                }

                return _cache;
            }
        }
    }
}

