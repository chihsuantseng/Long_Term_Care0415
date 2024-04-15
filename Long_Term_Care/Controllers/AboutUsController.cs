using Microsoft.AspNetCore.Mvc;

namespace Long_Term_Care.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult about_vision()
        {
            return View();
        }
        public IActionResult about_introduction()
        {
            return View();
        }
    }
}
