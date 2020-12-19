using MyWebFoundation.Test.Data;
using MyWebFoundation.Test.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebFoundation.Webserver.Controllers
{
    public class DapperTestController : Controller
    {
        IUserDepartService service;
        public DapperTestController(IUserDepartService _service)
        {
            service = _service;
        }

        [HttpGet]
        public JsonResult GetUser(int id)
        {
            tb_User result = service.Get<tb_User>(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CreateUserDepart()
        {
            tb_Department depart = new tb_Department();
            depart.Name = "depart1";
            depart.Description = "1";

            tb_User user = new tb_User();
            user.UserName = "user1";
            user.Address = "1";
            var result = service.InsertUserDepart(user, depart);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}