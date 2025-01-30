using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // Ana sayfa için yönlendirme
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
