using System.Web;
using System.Web.Mvc;

namespace TestingLayout.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        
        public ActionResult LanguageChange()
        {
            HttpCookie c = Request.Cookies["Language"];

            if (c.Value == "en")
                c.Value = "cs";
            else
                c.Value = "en";

            Response.Cookies.Add(c);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}