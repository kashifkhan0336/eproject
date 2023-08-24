using Eproject.Data;
using Microsoft.AspNetCore.Mvc;

namespace Eproject.Areas.User.Controllers
{
    [Area("User")]
    public class FaqController : Controller
    {
        private readonly ILogger<FaqController> _logger;
        private readonly EprojectContext _context;
        public FaqController(EprojectContext context, ILogger<FaqController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var faqEntries = _context.FaqEntries;
            return View(faqEntries);
        }
    }
}
