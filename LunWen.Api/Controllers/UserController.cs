using LunWen.Infrastructure;
using LunWen.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LunWen.Api.Controllers
{
    public class UserController : ApiController
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        public IHttpActionResult GetAll(string usercode)
        {
            try
            {
                if (string.IsNullOrEmpty(usercode))
                    return Ok(new BaseApiResponse() { Status = 401, Msg = "userCode不能为空" });

                var userInfo = _userService.GetUserByCode(usercode);
                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return Ok(new BaseApiResponse() { Status = 500, Msg = "内部错误" });
            }

            //todo：这里需要全部加上status
        }
    }
}
