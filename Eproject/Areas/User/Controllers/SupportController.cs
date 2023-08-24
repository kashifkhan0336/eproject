using Microsoft.AspNetCore.Mvc;

namespace Eproject.Areas.User.Controllers
{

    public class SupportController : Controller
    {
        [Area("User")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
