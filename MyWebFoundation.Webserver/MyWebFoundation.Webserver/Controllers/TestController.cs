using MyWebFoundation.Test.Data;
using MyWebFoundation.Test.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebFoundation.Webserver.Controllers
{
    public class TestController : Controller
    {
        IUserDepartModule module;
        public TestController(IUserDepartModule _module)
        {
            module = _module;
        }
        [HttpGet]
        public JsonResult GetDepartAssembleByDepartId(int id)
        {
            tb_Department result= module.GetDepartAssembleByDepartId(id);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}