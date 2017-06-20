using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Repository.baseDAO
{
    public interface IBaseRepository<T> where T : class
    {
        int Insert(T model);
        void Delete(int id);
        void DeleteAll(IEnumerable<int> ids);

        void Update(T model);

        //更新某类记录的某个状态时使用
        void UpdateBy(Dictionary<string, string> destFields, Dictionary<string, string> whereFields);

        //更新时先获取某个对象的值
        T SelectBy(int Id);

        //根据某些字段查找记录，比如登录名不能重复的验证就可以用到。
        IEnumerable<T> SelectBy(Dictionary<string, string> fields);


        //分页
    }

    public interface IBaseInfoRepository<T> where T : class
    {
        //查看某个对象的详情时使用
        T1 SelectInfoBy<T1>(int Id);
        IEnumerable<T1> SelectInfoBy<T1>(Dictionary<string, string> fields);
    }
}
