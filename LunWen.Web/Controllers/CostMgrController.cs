using LunWen.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Controllers
{
    public class CostMgrController : Controller
    {
        private int currentIndex = (int)MenuIdEnum.CostMgrIndex;

        public ActionResult Index()
        {
            ViewBag.CurrentIndex = currentIndex;

            return View();
        }
    }
}