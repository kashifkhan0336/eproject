using Eproject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            var supportInformation = await _context.SupportInformation.FirstOrDefaultAsync();
            return View(supportInformation);
        }
    }
}
