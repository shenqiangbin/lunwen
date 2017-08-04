using LunWen.Enums;
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
    public class OrgMgrController : Controller
    {
        private MenuService _menuService;
        private int currentIndex = (int)MenuIdEnum.OrgMgrIndex;

        public OrgMgrController(MenuService menuService)
        {
            _menuService = menuService;
        }

        public ActionResult Index()
        {
            ViewBag.CurrentIndex = currentIndex;

            Menu menu = _menuService.GetFirstMenu(ContextUser.RoleId, currentIndex);
            if (menu != null)
                return Redirect(menu.MenuUrl);
            else
                return View();
        }

        //角色管理
        public ActionResult RoleMgr()
        {
            ViewBag.CurrentIndex = currentIndex;
            return View();
        }

        //组织管理
        public ActionResult OrgMrg()
        {
            ViewBag.CurrentIndex = currentIndex;
            return View();
        }
    }
}