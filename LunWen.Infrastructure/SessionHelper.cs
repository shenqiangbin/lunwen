using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace LunWen.Infrastructure
{
    public class SessionHelper
    {
        public void Store(string ticketName, string userData)
        {
            DateTime expireTime = DateTime.Now.AddDays(1);
            string cookiePath = GetCookiePath();

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, ticketName, DateTime.Now, expireTime, false, userData, cookiePath);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private static string GetCookiePath()
        {
            if (HttpContext.Current.Request.IsLocal)
                return "/";
            else
                return HttpContext.Current.Request.Url.Host;
        }

        public static void SetUser()
        {
            var user = GetPrincipal();
            if (user != null)
                HttpContext.Current.User = user;
        }

        private static IPrincipal GetPrincipal()
        {
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            return new GenericPrincipal(new FormsIdentity(ticket), null);
        }
    }
}
