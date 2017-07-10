using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Cache
{
    public class NullCache : ICache
    {
        public T Get<T>(string key) where T : class
        {
            return null;
        }

        public void Remove(string key)
        {
            
        }

        public void Store(string key, object value)
        {
            
        }
    }
}
