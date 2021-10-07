using System.Web.Mvc;

namespace PaintballStore.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Shop()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
           

            return View();
        }
    }
}
