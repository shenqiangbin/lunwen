using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LunWen.Infrastructure.Cache;
using LunWen.Infrastructure;
using LunWen.Service;
using LunWen.Model;
using LunWen.Web.Common;

namespace LunWen.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private UserService _userService;
        private MenuService _menuService;

        public HomeController(UserService userService, MenuService menuService)
        {
            _userService = userService;
            _menuService = menuService;
        }

        public ActionResult Index()
        {
            if (ContextUser.IsLogined)
                return GoUrl();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string usercode, string password, string validateCode, string tmpToken, string returnUrl)
        {
            try
            {
                ViewBag.UserCode = usercode;
                ViewBag.Password = password;
                ValidateIndex(usercode, password, validateCode, tmpToken);

                if (CheckUser(usercode, password))
                {
                    SessionHelper.Store(usercode, string.Empty);
                    return GoUrl();
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

        private void ValidateIndex(string usercode, string password, string validateCode, string tmpToken)
        {
            var tmpTokenInServer = TempData["TmpToken"];
            if (tmpTokenInServer == null || tmpTokenInServer.ToString() != tmpToken)
                throw new ValidateException(403, "页面过期，请刷新");

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
            var user = _userService.GetUserByCode(usercode);
            if (user == null)
                return false;

            var passwordDecrypt = RSAHelper.Decrypt(password, TempData["RSAKey"].ToString());

            if (HashHelper.HashMd5(passwordDecrypt, user.Salt) != user.Password)
                return false;

            return true;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            LunWen.Infrastructure.SessionHelper.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult UnAuthrize()
        {
            return Content("没有访问权限");
        }


        private ActionResult GoUrl()
        {
            Menu menu = _menuService.GetFirstMenu(ContextUser.RoleId, 0);
            if (menu != null)
                return Redirect(menu.MenuUrl);
            else
                return Redirect("/");
        }
    }
}