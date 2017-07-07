using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LunWen.Infrastructure
{
    public class NetHelper
    {
        public static string Get(string url)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

                request.Method = "GET";
                WebResponse response = request.GetResponse();
                var stream = response.GetResponseStream();
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    var content = sr.ReadToEnd();
                    return content;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return "异常";
            }
        }

        public static string Post(string url, object requestData)
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

                request.Method = "Post";
                request.ContentType = "application/json";

                string body = JsonHelper.SerializeObject(requestData);
                byte[] bytes = Encoding.UTF8.GetBytes(body);
                request.GetRequestStream().Write(bytes, 0, bytes.Length);

                WebResponse response = request.GetResponse();
                var stream = response.GetResponseStream();
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    var content = sr.ReadToEnd();
                    return content;
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return "异常";
            }
        }
    }

    public class AppNetHelper
    {
        public static T Get<T>(string url) where T : class
        {
            var signUrl = AddSign(url);
            string content = NetHelper.Get(signUrl);
            return JsonHelper.DeserializeJsonToObject<T>(content);
        }

        private static string AddSign(string url)
        {
            List<string> para = new List<string>();
            string query = url.Substring(url.IndexOf("?"));
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

            if (url.Contains("?"))
                return string.Format("{0}&appkey={1}&sign={2}", url, appKey, sign);
            else
                return string.Format("{0}?appkey={1}&sign={2}", url, appKey, sign);
        }

        public static T Post<T>(string url, object requestData) where T : class
        {
            AddSignData(requestData);
            string content = NetHelper.Post(url, requestData);
            return JsonHelper.DeserializeJsonToObject<T>(content);
        }

        private static void AddSignData(object requestData)
        {
            List<string> para = new List<string>();
            foreach (var item in requestData.GetType().GetProperties())
            {
                var val = item.GetValue(requestData);
                if (string.IsNullOrEmpty(val.ToString()))
                    para.Add(val.ToString());
            }
            para.Sort();

            string appSecret = "s+QG+0CIX0G0T22pw+I+jw";
            ((BaseApiRequest)requestData).Appkey = "44j2scyyl4rdrtaj4cdm0f";
            ((BaseApiRequest)requestData).Sign = HashHelper.HashMd5(string.Join(",", para), appSecret);
        }
    }

    public class BaseApiRequest
    {
        public string Appkey { get; set; }
        public string Sign { get; set; }
    }

    public class BaseApiResponse
    {
        public int Status { get; set; }
        public string Msg { get; set; }
    }
}
