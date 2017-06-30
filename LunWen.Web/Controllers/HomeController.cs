using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LunWen.Infrastructure.Cache;
using LunWen.Infrastructure;

namespace LunWen.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string usercode, string password, string validateCode, string returnUrl)
        {

            try
            {
                ViewBag.UserCode = usercode;
                ViewBag.Password = password;
                ValidateIndex(usercode, password, validateCode);

                if (usercode == "admin" && password == "123")
                {
                   return RedirectToAction("index", "user");
                }
                else
                {
                    ViewBag.Msg = "用户名或密码错误";
                }
            }
            catch (ValidateException ex)
            {
                ViewBag.Msg = ex.Message;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                ViewBag.Msg = "登录失败(500)";
            }

            return View();
        }

        private void ValidateIndex(string usercode, string password, string validateCode)
        {
            if (string.IsNullOrEmpty(usercode))
                throw new ValidateException(400, "登录名不能为空");
            if (string.IsNullOrEmpty(password))
                throw new ValidateException(401, "密码不能为空");
            if (string.IsNullOrEmpty(validateCode))
                throw new ValidateException(402, "验证码不能为空");

            if (Session["Code"] != null)
            {
                if (!string.Equals(validateCode, Session["Code"].ToString(), StringComparison.CurrentCultureIgnoreCase))
                    throw new ValidateException(402, "验证码不正确");
            }
        }
    }
}