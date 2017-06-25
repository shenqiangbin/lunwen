using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Service
{
    public interface IBaseService
    {
        int Add<T>(T model);
        void Remove<T>(T model);
        void Save<T>(T model);
    }
}
