using AccountManager.WebApp.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AccountManager.WebApp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        //[ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult Index()
        {
            
            return View();
        }
        [CustomAuthorize(PermissionNames = "TestIndex")]//use test/index permission
        public ActionResult GetCode1()
        {
            var iden = User.Identity as FormsIdentity;
            return Json(new {
                id=int.MaxValue,
                name=Guid.NewGuid().ToString()
            },JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult GetCode2()
        {
            return Json(new
            {
                id = int.MaxValue,
                name = Guid.NewGuid().ToString()
            }, JsonRequestBehavior.AllowGet);
        }
    }
}