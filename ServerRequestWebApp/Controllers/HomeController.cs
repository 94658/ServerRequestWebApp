using ServerRequestWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServerRequestWebApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //[Authorize]
        public ActionResult Index()
        {
            // TempData["registered"] = false;

            //for the Administrator
           
            var pendingRequests= db.ServerAccessModels.Where(x =>  x.isPending==true && x.approved_by==null).Count();
            var rejectedRequests = db.ServerAccessModels.Where(x => x.approved_by != null && x.supervised_by != null&& x.isSupervised == true && x.isApproved == false).Count();
            ViewBag.username = User.Identity.Name;
            ViewBag.requests = db.ServerAccessModels.Count();
            ViewBag.pending = pendingRequests;
            ViewBag.rejected = rejectedRequests;


            //for the Others - Who make requests
            var requestsmade = db.ServerAccessModels.Where(x => x.created_by == MySession.Current.userName).Count();
            ViewBag.requestsmade = requestsmade;
            var approved = db.ServerAccessModels.Where(x => x.created_by == MySession.Current.userName && x.isApproved == true).Count();
            ViewBag.approvedrequests = approved;

           var pending = db.ServerAccessModels.Where(x => x.created_by == MySession.Current.userName && x.isPending == true ).Count();
            ViewBag.pendingrequests = pending;
           var reject = db.ServerAccessModels.Where(x => x.created_by == MySession.Current.userName && x.approved_by != null && x.supervised_by != null && x.isSupervised == true && x.isApproved == false).Count();
            ViewBag.rejectedrequests = reject;
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

        [Authorize]
        public virtual ActionResult ServerRequestForm()
        {

            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ServerRequestForm(ServerAccessModel serverAccessModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    serverAccessModel.created_by = MySession.Current.userName;
                    serverAccessModel.created_on = DateTime.Now;
                    serverAccessModel.supervised_by = null;
                    serverAccessModel.supervised_on = null;
                    serverAccessModel.approved_by = null;
                    serverAccessModel.approved_on = null;
                    serverAccessModel.isPending = true;
                    serverAccessModel.isApproved = false;
                    serverAccessModel.isSupervised = false;
                    db.ServerAccessModels.Add(serverAccessModel);
                    await db.SaveChangesAsync();
                    TempData["UserMessage"] = new MessageVM() { CssClassName = "alert-sucess", Title = "Success!", Message = "Operation Done." };
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    TempData["UserMessage"] = new MessageVM() { CssClassName = "alert-error", Title = "Error!", Message = "Operation Failed." };
                    return RedirectToAction("Index");
                }
                
            }
           
            return View(serverAccessModel);
        }


        [Authorize]
        public ActionResult ServerRequests()
        {
           
            return View();
        }

        [Authorize]
        public ActionResult ServerAccessRequests()
        {
            var IsApprover = db.Admins.Where(x => x.AdminName == User.Identity.Name).FirstOrDefault().Approver;
            ViewBag.isapprover=IsApprover;
            return View(db.ServerAccessModels.ToList());
        }

        [Authorize]
        public ActionResult SupervisorApprove(int id)
        {
            return View(db.ServerAccessModels.Where(s => s.ID == id).First());
        }

        [HttpPost]
        public ActionResult SupervisorApproverRequests()
        {
            return View();
        }

    }
}