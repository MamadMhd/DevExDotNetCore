using Microsoft.AspNetCore.Mvc;

namespace DevExtremeAspNetCoreApp1.Controllers
{
    public class BookViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
