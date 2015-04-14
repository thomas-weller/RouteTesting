using System.Web.Mvc;

namespace RouteTesting.Mvc.Controllers
{
    public class RenamedFooController : Controller
    {
        public ActionResult Bar()
        {
            return View();
        }
    }
}
