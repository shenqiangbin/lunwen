﻿using LunWen.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Controllers
{
    public class ManagerController : Controller
    {
        private int currentIndex = (int)MenuIdEnum.ManagerIndex;

        public ActionResult Index()
        {
            ViewBag.CurrentIndex = currentIndex;
            return View();
        }
    }
}