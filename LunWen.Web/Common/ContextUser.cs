using Autofac.Integration.Mvc;
using LunWen.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LunWen.Web.Common
{
    public class ContextUser
    {
        public static string UserCode
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    return HttpContext.Current.User.Identity.Name;
                else
                    return "";
            }
        }

        public static bool IsLogined
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public static string UserName
        {
            get
            {
                var userService = AutofacDependencyResolver.Current.GetService(typeof(UserService)) as UserService;
                var user = userService.GetUserByCode(ContextUser.UserCode);
                if (user != null)
                    return user.UserName;
                else
                    return "";
            }
        }

        public static int RoleId
        {
            get
            {
                var userService = AutofacDependencyResolver.Current.GetService(typeof(UserService)) as UserService;
                var user = userService.GetUserByCode(ContextUser.UserCode);
                if (user != null)
                    return user.RoleId;
                else
                    return -1;
            }
        }
    }
}
