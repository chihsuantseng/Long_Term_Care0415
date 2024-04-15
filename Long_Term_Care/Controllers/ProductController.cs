using Microsoft.AspNetCore.Mvc;

namespace Long_Term_Care.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Product()
        {
            return View();
        }
    }
}
