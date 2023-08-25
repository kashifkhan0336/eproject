using Eproject.Data;
using Microsoft.AspNetCore.Mvc;

namespace Eproject.Areas.User.Controllers
{
    [Area("User")]
    public class SupportController : Controller
    {

        private readonly ILogger<SupportController> _logger;
        private readonly EprojectContext _context;
        public SupportController(EprojectContext context, ILogger<SupportController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
