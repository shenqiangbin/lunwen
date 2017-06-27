using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Sex { get; set; }
    }

    public class UserSaveModel
    {
        public int? Id { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? Sex { get; set; }

        public void SetValTo(User model)
        {
            model.UserCode = UserCode != null ? UserCode : model.UserCode;
            model.UserName = UserName != null ? UserName : model.UserName;
            model.Password = Password != null ? Password : model.Password;
            model.Phone = Phone != null ? Phone : model.Phone;
            model.Email = Email != null ? Email : model.Email;
            model.Sex = Sex != null ? Sex.Value : model.Sex;
        }
    }
    
    //列表项时使用
    public class UserItem
    {
        public int Id { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Sex { get; set; }
    }

    //详情时使用
    public class UserInfo
    {

    }
}
