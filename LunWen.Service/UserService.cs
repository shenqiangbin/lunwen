using LunWen.Model;
using LunWen.Model.Request;
using LunWen.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Service
{
    public class UserService
    {
        private UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int Add(User model)
        {
            return _userRepository.Insert(model);
        }

        public void Remove(int id)
        {
            _userRepository.Delete(id);
        }

        public void Save(UserSaveModel saveModel)
        {
            if (!saveModel.Id.HasValue)
                throw new Exception("id不能为空");

            User model = _userRepository.SelectBy(saveModel.Id.Value);
            if (model == null)
                throw new Exception("id不存在");

            saveModel.SetValTo(model);

            _userRepository.Update(model);
        }

        public QueryResult<UserItem> Get(UserQuery query)
        {
            return _userRepository.Get(query);
        }

        public UserInfo GetUserByCode(string userCode)
        {
            IEnumerable<UserInfo> users = _userRepository.GetUserByCode(userCode);

            if (users != null && users.Count() > 1)
                throw new Exception("usercode有重复：" + userCode);

            return users.FirstOrDefault();
        }
    }
}
