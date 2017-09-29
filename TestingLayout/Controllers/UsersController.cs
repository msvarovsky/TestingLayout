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

        // GET: Users
        public ActionResult AllUsers()
        {
            uvm = new UsersViewModel();
            uvm.Load();
            return View(uvm);
        }

        [HttpGet]
        public ActionResult NewUser()
        {



            SexGlossary sg = new SexGlossary();
            sg.GetAllSexes("en");

            return View(sg);
        }

        [HttpPost]
        public ActionResult NewUser(FormCollection fc)
        {
            uvm = new UsersViewModel();

            string ret = uvm.Add(new Models.User { Name = fc["Name"], Sex = fc["Sex"] });
            if (ret != null)
                return Content(ret);
            return RedirectToAction("AllUsers");
        }
    }
}