using AutoMapper;
using Eproject.Areas.Controllers.Admin;
using Eproject.Data;
using Eproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Eproject.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class SurveysController : Controller
    {
        private readonly ILogger<SurveysController> _logger;
        private readonly EprojectContext _context;
        private readonly UserManager<EprojectUser> _userManager;

        public SurveysController(EprojectContext context, ILogger<SurveysController> logger, IMapper mapper, UserManager<EprojectUser> userManager)
        {
            _context = context;
            _logger = logger;

            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var participatedSurveyIds = await _context.SurveyEprojectUsers
                .Where(surveyUser => surveyUser.EprojectUserId == userId)
                .Select(surveyUser => surveyUser.SurveyId)
                .ToListAsync();

            var surveys = await _context.Surveys
                .Include(s => s.Allowed)
                .Include(s => s.Participants)
                .Include(s => s.Completions)
                .ThenInclude(s => s.User)
                .ToListAsync();



            var notParticipatedSurveys = surveys
                .Where(survey => !participatedSurveyIds.Contains(survey.Id))
                .ToList();
            ViewBag.NotParticipatedSurveys = notParticipatedSurveys;
            return View(surveys);
        }
    }
}
