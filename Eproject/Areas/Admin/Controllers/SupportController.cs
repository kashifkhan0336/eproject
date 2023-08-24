using Microsoft.AspNetCore.Mvc;

namespace Eproject.Areas.Controllers.Admin
{
    public class SupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
