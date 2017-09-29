using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingLayout.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult AllUsers()
        {
            return View();
        }
    }
}