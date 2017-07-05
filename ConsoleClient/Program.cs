using LunWen.Cache;
using LunWen.Infrastructure;
using LunWen.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetRandomStr();
            //GetSomePassword();
            TestCache();
            Console.ReadKey();
        }

        private static void GetSomePassword()
        {
            StringBuilder builder = new StringBuilder();

            List<string> pwds = new List<string>()
           {
               "123","123456"
           };

            foreach (var pwd in pwds)
            {
                var salt = Guid.NewGuid().ToString();
                var hashPwd = HashHelper.HashMd5(pwd, salt);
                builder.AppendFormat("{0}  {1}  {2} \r\n", pwd, salt, hashPwd);
            }

            System.IO.File.WriteAllText("d:/pwd.txt", builder.ToString());
            Process.Start("d:/pwd.txt");
        }


        private static void GetRandomStr()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", ""));
            }
        }

        private static void TestCache()
        {
            UserInfo user = new UserInfo();
            user.Id = 1;
            user.UserCode = "admin";
            user.UserName = "admin";

            ICache cache = new ReidsCache();
            cache.Store("user", user);

            var userInCache = cache.Get<User>("user");

            cache.Store("users", new List<UserInfo> { new UserInfo { UserCode = "admin" }, new UserInfo { UserCode = "user" } });
            var usersInCache = cache.Get<IEnumerable<User>>("users");

        }
    }
}
