using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingLayout.Models;

namespace TestingLayout.Controllers
{
    public class UsersController : Controller
    {
        UsersViewModel uvm;
        NewUserViewModel nuvm;
        // GET: Users
        public ActionResult AllUsers()
        {
            uvm = new UsersViewModel(Request.Cookies["Language"].Value);
            return View(uvm);
        }

        [HttpGet]
        public ActionResult NewUser()
        {
            NewUserViewModel nu = new NewUserViewModel(Request.Cookies["Language"].Value);
            ViewBag.Language = Request.Cookies["Language"].Value.ToString();
            ViewBag.Genders = nu.gender;
            ViewBag.Labels = nu.SystemLabels;

            //return View(nu);
            User u = new Models.User("newuser", Request.Cookies["Language"].Value.ToString());
            u.Sex = "m";
            return View(u);
        }

        [HttpPost]
        public ActionResult NewUser(User u)
        {
            return RedirectToAction("AllUsers");
        }

        /*public ActionResult NewUser(FormCollection fc)
        {
            string aa = fc["gender"];
            nuvm = new NewUserViewModel(Request.Cookies["Language"].Value);
            string ret = nuvm.Add(new Models.User { Name = fc["Name"], Sex = fc["Sex"] });

            if (ret != null)
                return Content(ret);

            return RedirectToAction("AllUsers");
        }*/
    }
}