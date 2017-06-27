using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Infrastructure.Cache
{
    public interface ICache
    {
        void Store(string key, object value);
        object Get(string key);
        void Remove(string key);
    }
}
