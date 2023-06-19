using Microsoft.AspNetCore.Mvc;

namespace SchneiderStore.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
