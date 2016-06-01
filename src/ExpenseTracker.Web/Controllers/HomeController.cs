using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    [AllowAnonymous()]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
