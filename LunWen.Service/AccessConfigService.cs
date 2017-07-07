using LunWen.Cache;
using LunWen.Model;
using LunWen.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Service
{
    public class AccessConfigService
    {
        private AccessConfigRepository _accessConfigRepository;

        public AccessConfigService(AccessConfigRepository accessConfigRepository)
        {
            _accessConfigRepository = accessConfigRepository;
        }

        public string GetAppSecret(string appKey)
        {
            IEnumerable<AccessConfig> configs = CacheManager.Cache.Get<IEnumerable<AccessConfig>>(KeyManager.GetAccessConfigKey()); ;
            if (configs == null)
            {
                configs = _accessConfigRepository.SelectBy(new Dictionary<string, string>() { { "status", "1" } });
                CacheManager.Cache.Store(KeyManager.GetAccessConfigKey(), configs);
            }

            var config = configs.FirstOrDefault(m => m.AppKey == appKey);
            if (config == null)
                return "";
            else
                return config.AppSecret;

        }
    }
}
