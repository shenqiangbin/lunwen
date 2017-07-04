using LunWen.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Controllers
{
    [AllowAnonymous]
    public class DbController : Controller
    {
        private SqlService _sqlService;

        public DbController()
        {
            _sqlService = new SqlService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InitDB()
        {
            try
            {
                var filePath = Server.MapPath("~/sql/InitDB.sql");
                if (_sqlService.ExeTran(filePath))
                    return Content("初始化成功");
                else
                    return Content("初始化失败");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult InitData()
        {
            try
            {
                var filePath = Server.MapPath("~/sql/InitData.sql");
                if (_sqlService.ExeTran(filePath))
                    return Content("初始化成功");
                else
                    return Content("初始化失败");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Info()
        {
            var dbInfo = _sqlService.GetDbInfo();
            return View(dbInfo);
        }
    }
}