﻿using LunWen.Infrastructure;
using LunWen.Model;
using LunWen.Model.Request;
using LunWen.Service;
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

        public UserController()
        {
            _userService = new UserService();
        }

        public ActionResult Index(string username, int page = 1)
        {
            int itemsPerPage = 3;

            var query = new UserQuery();
            query.UserName = username;
            query.PageCondition = new PageCondition(page, itemsPerPage);

            try
            {
                QueryResult<UserItem> response = _userService.Get(query);
                ViewBag.UserResult = response;

                if ((response.List == null || response.List.Count() == 0) && page != 1)
                    return RedirectToAction("index", "user", new { username = username, page = --page });

                //var pageList = new StaticPagedList<News>(response.List, page, itemsPerPage, response.TotalCount);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }

            return View();
        }
    }
}