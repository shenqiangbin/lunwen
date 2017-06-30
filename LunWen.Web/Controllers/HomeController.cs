using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LunWen.Infrastructure.Cache;
using LunWen.Infrastructure;
using LunWen.Service;
using LunWen.Model;

namespace LunWen.Web.Controllers
{
    public class HomeController : Controller
    {
        private UserService _userService;

        public HomeController(UserService userService)
        {
            _userService = userService;
        }

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

                if (CheckUser(usercode, password))
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

        private bool CheckUser(string usercode, string password)
        {
            User user = _userService.GetUserByCode(usercode);
            if (user == null)
                return false;

            if (HashHelper.HashMd5(password, user.Salt) != user.Password)
                return false;

            return true;
        }
    }
}