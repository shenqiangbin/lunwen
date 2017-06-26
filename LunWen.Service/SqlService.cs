using LunWen.Repository;
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
    }
}
