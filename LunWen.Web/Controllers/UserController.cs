using LunWen.Enums;
using LunWen.Infrastructure;
using LunWen.Model;
using LunWen.Model.Request;
using LunWen.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LunWen.Web.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService;
        private int currentIndex = (int)MenuIdEnum.UserIndex;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index(string username, int page = 1)
        {
            ViewBag.CurrentIndex = currentIndex;

            int itemsPerPage = 3;

            var query = new UserQuery();
            query.UserName = username;
            query.PageCondition = new PageCondition(page, itemsPerPage);

            try
            {
                QueryResult<UserItem> response = _userService.Get(query);

                if ((response.List == null || response.List.Count() == 0) && page != 1)
                    return RedirectToAction("index", "user", new { username = username, page = --page });

                var pageList = new StaticPagedList<UserItem>(response.List, page, itemsPerPage, response.TotalCount);
                ViewBag.UserResult = pageList;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }

            ViewBag.UserName = username;

            return View();
        }
    }
}