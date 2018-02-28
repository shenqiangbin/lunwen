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
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8"; //这里必须是这个，在过滤器中才能通过form.keys获取到参数

                List<string> para = new List<string>();
                foreach (var item in requestData.GetType().GetProperties())
                {
                    var val = item.GetValue(requestData);
                    //if (!string.IsNullOrEmpty(val.ToString()))
                    para.Add(item.Name + "=" + val.ToString());
                }

                //string body = JsonHelper.SerializeObject(requestData); //如果type是json格式的话，可以用这句。
                string body = string.Join("&", para.ToList());

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
        public static BaseApiResponse<T> Get<T>(string url) where T : class
        {
            var signUrl = AddSign(url);
            string content = NetHelper.Get(signUrl);
            var response = JsonHelper.DeserializeJsonToObject<BaseApiResponse<T>>(content);
            return response;
        }

        private static string AddSign(string url)
        {
            var time = DateTime.Now.Ticks.ToString();

            List<string> para = new List<string>();
            string query = url.Substring(url.IndexOf("?")+1);
            foreach (var item in query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var keyvalue = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyvalue.Count() >= 2)
                    para.Add(keyvalue[1]);
            }

            para.Add(time);
            para.Sort();

            string appSecret = "s+QG+0CIX0G0T22pw+I+jw";
            string appKey = "44j2scyyl4rdrtaj4cdm0f";
            string sign = HashHelper.HashMd5(string.Join(",", para), appSecret);

            if (url.Contains("?"))
                return string.Format("{0}&appkey={1}&sign={2}&time={3}", url, appKey, sign, time);
            else
                return string.Format("{0}?appkey={1}&sign={2}&time={3}", url, appKey, sign, time);
        }

        public static BaseApiResponse<T> Post<T>(string url, object requestData) where T : class
        {
            url = AddSignData(requestData, url);
            string content = NetHelper.Post(url, requestData);
            var response = JsonHelper.DeserializeJsonToObject<BaseApiResponse<T>>(content);
            return response;
        }

        private static string AddSignData(object requestData, string url)
        {
            var time = DateTime.Now.Ticks.ToString();

            List<string> para = new List<string>();
            foreach (var item in requestData.GetType().GetProperties())
            {
                var val = item.GetValue(requestData);
                if (!string.IsNullOrEmpty(val.ToString()))
                    para.Add(val.ToString());
            }
            para.Add(time);
            para.Sort();

            //string appSecret = "s+QG+0CIX0G0T22pw+I+jw";
            //((BaseApiRequest)requestData).Appkey = "44j2scyyl4rdrtaj4cdm0f";
            //((BaseApiRequest)requestData).Sign = HashHelper.HashMd5(string.Join(",", para), appSecret);

            string appSecret = "s+QG+0CIX0G0T22pw+I+jw";
            string appKey = "44j2scyyl4rdrtaj4cdm0f";
            string sign = HashHelper.HashMd5(string.Join(",", para), appSecret);

            if (url.Contains("?"))
                return string.Format("{0}&appkey={1}&sign={2}&time={3}", url, appKey, sign, time);
            else
                return string.Format("{0}?appkey={1}&sign={2}&time={3}", url, appKey, sign, time);
        }
    }

    public class BaseApiRequest
    {
        public string Appkey { get; set; }
        public string Sign { get; set; }
    }

    public class BaseApiResponse<T>
    {
        public int Status { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }
    public class BaseApiResponse
    {
        public int Status { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }

}
