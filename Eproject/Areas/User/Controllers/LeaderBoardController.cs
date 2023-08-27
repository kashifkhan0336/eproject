using Eproject.Data;
using Eproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Areas.User.Controllers
{
    [Area("User")]
    public class LeaderBoardController : Controller
    {
        private readonly ILogger<LeaderBoardController> _logger;
        private readonly EprojectContext _context;

        public LeaderBoardController(EprojectContext context, ILogger<LeaderBoardController> logger, UserManager<EprojectUser> userManager)
        {
            _context = context;
            _logger = logger;

        }
        public async Task<IActionResult> Index()
        {
            var surveysWithCompletions = await _context.Surveys
                .Include(s => s.Allowed)
                .Include(s => s.Participants)
                .Include(s => s.Completions)
                .ThenInclude(s => s.User)

                .ToListAsync();
            return View(surveysWithCompletions);
        }
    }
}
