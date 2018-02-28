using Autofac.Integration.WebApi;
using LunWen.Infrastructure;
using LunWen.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace LunWen.Api.Filters
{
    public class WebApiAuthAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            string time = null;
            string appKey = null;
            string appSecret = null;
            string sign = null;
            List<string> paraList = new List<string>();

            if (actionContext.Request.Method == HttpMethod.Get)
            {
                var paras = actionContext.Request.GetQueryNameValuePairs();
                foreach (var item in paras)
                {
                    if ("sign" == item.Key.ToLower())
                        sign = item.Value;
                    else if ("appkey" == item.Key.ToLower())
                        appKey = item.Value;
                    else if ("time" == item.Key.ToLower())
                        time = item.Value;
                    else if (!string.IsNullOrEmpty(item.Value))
                        paraList.Add(item.Value);
                }
            }
            else if (actionContext.Request.Method == HttpMethod.Post)
            {
                HttpContextBase context = (HttpContextBase)actionContext.Request.Properties["MS_HttpContext"];
                HttpRequestBase request = context.Request;

                foreach (var item in request.Form.AllKeys)
                {
                    if (!string.IsNullOrEmpty(request.Form[item]))
                        paraList.Add(request.Form[item]);
                }

                var paras = actionContext.Request.GetQueryNameValuePairs();
                foreach (var item in paras)
                {
                    if ("sign" == item.Key.ToLower())
                        sign = item.Value;
                    else if ("appkey" == item.Key.ToLower())
                        appKey = item.Value;
                    else if ("time" == item.Key.ToLower())
                        time = item.Value;
                }
            }

            if(string.IsNullOrEmpty(sign) || string.IsNullOrEmpty(appKey) || string.IsNullOrEmpty(time))
            {
                return false;
            }

            var postTime = new DateTime(Convert.ToInt64(time));
            if ((DateTime.Now - postTime).TotalMinutes >= 3)
            {                
                return false;
            }

            if (string.IsNullOrEmpty(appKey) || string.IsNullOrEmpty(sign))
                return false;

            paraList.Add(time);
            paraList.Sort();

            appSecret = GetAppKey(appKey);
            if (string.IsNullOrEmpty(appSecret))
                return false;

            var signCal = HashHelper.HashMd5(string.Join(",", paraList), appSecret);
            return signCal == sign;
        }

        private string GetAppKey(string appKey)
        {
            var accessConfigService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(AccessConfigService)) as AccessConfigService;
            return accessConfigService.GetAppSecret(appKey);
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            string msg = JsonHelper.SerializeObject(new BaseApiResponse() { Status = 403, Msg = "无权访问" });
            actionContext.Response.Content = new StringContent(msg);
        }
    }
}