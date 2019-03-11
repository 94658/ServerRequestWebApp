using Microsoft.Owin.Security;
using ServerRequestWebApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServerRequestWebApp.Controllers
{
    public class LoginController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //AdAuthenticationService adAuthenticationService = new AdAuthenticationService();

        // GET: Login
        [AllowAnonymous]
        public virtual ActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);

            var authenticationResult = authService.SignIn(model.Username, model.Password);
            bool IsAdmin = authService.admin;
            if (authenticationResult.IsSuccess)
            {
                // we are in
                //check if UserInfo is updated
                //todays edit
                //ApplicationDbContext db = new ApplicationDbContext();
                //bool userExists = 
                //var string user = User.Identity.Name;
                var userExist = db.UserProfile.Where(m => m.UserName == MySession.Current.userName).FirstOrDefault();
                //MySession.Current.ID = userExist.UserProfileId;
                if (userExist != null)
                {
                    //redirect to landing page
                    // Session["Username"] = authService.authUser;
                    //ViewBag.UserName = User.Identity.Name;
                    // return RedirectToAction("Index", "Home" );
                    MySession.Current.IsAdmin= userExist.isAdmin;
                    MySession.Current.ID = userExist.UserProfileId;
                    return RedirectToLocal(returnUrl);

                }
                else
                {
                    //register
                    //Session["Username"] = authService.authUser;
                    //TempData["IsAdmin"] = IsAdmin;
                    //Session["IsAdmin"]= IsAdmin;
                    ViewBag.UserName = User.Identity.Name;
                    return RedirectToAction("Register");

                }

               
             }
           
            else
            {
                ModelState.AddModelError(string.Empty, authenticationResult.ErrorMessage);
                return View(model);
                //ViewBag.FailMessage = authenticationResult.ErrorMessage;
              
            }

        }

        //ModelState.AddModelError("", authenticationResult.ErrorMessage);
        //return View(model);

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            ViewBag.userName = User.Identity.Name;
            //var loggedInUser = new UserProfileModel();
            
            //loggedInUser = db.UserProfile.Where(m => m.UserProfileId == MySession.Current.ID).FirstOrDefault();
            
            return  RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        public virtual ActionResult Logoff()
        {

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            return RedirectToAction("Index");
        }

        [Authorize]
        public virtual ActionResult Register()
        {

            ViewBag.Department = new SelectList(db.Departments, "DepartmentId", "Department");
            ViewBag.username = User.Identity.Name;
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserProfileModel model, string Department)
        {
            model.Department = Department;
            model.CreatedBy = User.Identity.Name; 
            model.UserName = User.Identity.Name;
            model.isAdmin = MySession.Current.IsAdmin;
            model.CreatedOn = DateTime.Now;
            db.UserProfile.Add(model);
            await db.SaveChangesAsync();
            return RedirectToAction("Index","Home"); 
        }

    }
    

    public class LoginViewModel
    {
        [Required, AllowHtml]
        public string Username { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    
}