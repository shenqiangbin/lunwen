using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Cache
{
    public interface ICache
    {
        void Store(string key, object value);
        T Get<T>(string key) where T : class;        
        void Remove(string key);
    }
}
