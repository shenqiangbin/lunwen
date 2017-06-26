using LunWen.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Filters
{
    public class ExcepitonFilter : IExceptionFilter
    {
        public bool AllowMultiple
        {
            get; set;
        }

        public void OnException(ExceptionContext filterContext)
        {
            try
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine(filterContext.Exception.Message);
                msg.AppendLine(filterContext.Exception.StackTrace);
                SqlLogger.Log(msg.ToString());
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }

}