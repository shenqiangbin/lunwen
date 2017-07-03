using LunWen.Infrastructure;
using LunWen.Web.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Filters
{
    public class TimingActionFilter : System.Web.Mvc.ActionFilterAttribute
    {
        private string para;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var str = filterContext.HttpContext.Request.HttpMethod;

            StringBuilder builder = new StringBuilder();
            foreach (var item in filterContext.ActionParameters)
            {
                builder.Append(item.Key + ":" + item.Value + ",");
            }
            para = builder.ToString();

            GetTimer(filterContext, "action").Start();
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            GetTimer(filterContext, "action").Stop();
            base.OnActionExecuted(filterContext);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var renderTimer = GetTimer(filterContext, "render");
            renderTimer.Stop();
            var actionTimer = GetTimer(filterContext, "action");
            if (actionTimer.ElapsedMilliseconds >= 100 || renderTimer.ElapsedMilliseconds >= 100)
            {
                LunWen.Infrastructure.Logger.Log("运营监控(" + filterContext.RouteData.Values["controller"] + ")" + String.Format(
                 "【{0}】-【{1}】,执行:{2}ms,渲染:{3}ms",
                 filterContext.RouteData.Values["controller"],
                 filterContext.RouteData.Values["action"],
                 actionTimer.ElapsedMilliseconds,
                 renderTimer.ElapsedMilliseconds
                 ));
            }
            TimeLogger.Log(
                ContextUser.UserCode,
                filterContext.RouteData.Values["controller"] + "/" + filterContext.RouteData.Values["action"],
                actionTimer.ElapsedMilliseconds.ToString(),
                renderTimer.ElapsedMilliseconds.ToString(),
                para);
            base.OnResultExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            GetTimer(filterContext, "render").Start();
            base.OnResultExecuting(filterContext);
        }
        private Stopwatch GetTimer(ControllerContext context, string name)
        {
            string key = "__timer__" + name;
            if (context.HttpContext.Items.Contains(key))
            {
                return (Stopwatch)context.HttpContext.Items[key];
            }
            var result = new Stopwatch();
            context.HttpContext.Items[key] = result;
            return result;
        }
    }
}