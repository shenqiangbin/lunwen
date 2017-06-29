using LunWen.Repository;
using LunWen.Repository.baseDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Service
{
    public class SqlService
    {
        private UserRepository _userRepository;

        public SqlService()
        {
            _userRepository = new UserRepository();
        }

        public bool ExeTran(string filePath)
        {
            string sql = System.IO.File.ReadAllText(filePath);
            return _userRepository.ExeTransaction(sql);
        }


        public Dictionary<Table, IEnumerable<Column>> GetDbInfo()
        {
            var dic = new Dictionary<Table, IEnumerable<Column>>();

            var tables = _userRepository.GetTables();
            foreach (var table in tables)
            {
                dic.Add(table, _userRepository.GetTableColumns(table.TableName));
            }

            return dic;
        }
    }
}
