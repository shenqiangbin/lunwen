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
                }
            }

            if (string.IsNullOrEmpty(appKey) || string.IsNullOrEmpty(sign))
                return false;

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
            //actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            //actionContext.Response.Content = new StringContent(JsonHelper.SerializeObject(new { status = 401, msg = "未授权"}));
        }
    }
}