using System.Net;
using System.Web.Mvc;

namespace WebNetBuilder.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ProjectSetting()
        {
            ViewBag.ServerURL = System.Web.Configuration.WebConfigurationManager.AppSettings["ServerURL"];
            return View();
        }
        public ActionResult ProjectStatus()
        {

            return View();
        }
        public ActionResult ProjectLog() {
            return View();
        }
    }
}