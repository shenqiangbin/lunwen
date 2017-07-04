using LunWen.Model;
using LunWen.Service;
using LunWen.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Controllers
{
    public class ThesisMgrController : Controller
    {
        private MenuService _menuService;
        private int currentIndex = 2;

        public ThesisMgrController(MenuService menuService)
        {
            _menuService = menuService;
        }

        //论文管理
        public ActionResult Index()
        {
            Menu menu = _menuService.GetFirstMenu(ContextUser.RoleId, 2);
            return Redirect(menu.MenuUrl);
        }

        //论文审核
        public ActionResult Check()
        {
            ViewBag.CurrentIndex = currentIndex;
            return View();
        }

        //批量提交
        public ActionResult BatchSubmit()
        {
            ViewBag.CurrentIndex = currentIndex;
            return View();
        }
    }
}