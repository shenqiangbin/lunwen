using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Filters
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        private bool isAuthorize;
        private string actionUrl;

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (isAuthorize)//如果认证了，只是没有权限则展示无权限界面
                filterContext.Result = new RedirectResult("/home/unAuthrize");
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var controllerName = (filterContext.RouteData.Values["controller"]).ToString().ToLower();
            var actionName = (filterContext.RouteData.Values["action"]).ToString().ToLower();
            var areaName = (filterContext.RouteData.DataTokens["area"] == null ? "" : filterContext.RouteData.DataTokens["area"]).ToString().ToLower();

            if (string.IsNullOrEmpty(areaName))
                actionUrl = "/" + controllerName + "/" + actionName;
            else
                actionUrl = "/" + areaName + "/" + controllerName + "/" + actionName;


            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            isAuthorize = base.AuthorizeCore(httpContext);
            if (isAuthorize)
            {
                bool canAccess = IsInRole(httpContext.User.Identity.Name, actionUrl);
                return canAccess;
            }
            else
            {
                return false;
            }

        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return base.OnCacheAuthorization(httpContext);
        }

        private bool IsInRole(string account, string url)
        {
            //todo:添加角色验证代码
            if (account != "admin" && url == "/user/index")
                return false;
            else
                return true;
        }
    }
}