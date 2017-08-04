using LunWen.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Controllers
{
    public class NoticeMgrController : Controller
    {
        private int currentIndex = (int)MenuIdEnum.NoticeMgrIndex;

        public ActionResult Index()
        {
            ViewBag.CurrentIndex = currentIndex;
            return View();
        }
    }
}