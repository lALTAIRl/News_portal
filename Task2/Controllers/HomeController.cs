using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using News_portal.ViewModels;

namespace News_portal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectPermanent("/News/NewsCollection");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
