﻿using LunWen.Model;
using LunWen.Service;
using LunWen.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Controllers
{
    [AllowAnonymous]
    public class NavController : Controller
    {
        private MenuService _menuService;

        public NavController(MenuService menuService)
        {
            _menuService = menuService;
        }

        public ActionResult Index(int i = 0)
        {
            int roleId = ContextUser.RoleId;
            IEnumerable<Menu> menus = _menuService.GetMenuByRole(roleId,0);
            ViewBag.Menus = menus as IList<Menu>;
            ViewBag.CurrentIndex = i;

            return PartialView();
        }
    }
}