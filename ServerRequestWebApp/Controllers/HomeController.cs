using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerRequestWebApp.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize]
        public ActionResult Index()
        {
            ViewBag.username = User.Identity.Name;
            return View();
        }

        [Authorize(Roles = "NonExistingRole")]
        public ActionResult ForNonExistingRole()
        {
            return View();
        }

        [Authorize(Roles = "Users")]
        public ActionResult OnlyForUsers()
        {
            return View();
        }

        [Authorize]
        public ActionResult MyClaims()
        {
            ViewBag.Test = User.Identity.Name;
            return View();
        }
    }
}