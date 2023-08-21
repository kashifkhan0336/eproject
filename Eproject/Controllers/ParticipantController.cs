using Eproject.Data;
using Eproject.Models;
using Eproject.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly ILogger<FaqController> _logger;
        private readonly EprojectContext _context;
        private readonly UserManager<EprojectUser> _userManager;

        public ParticipantController(EprojectContext context, ILogger<FaqController> logger, UserManager<EprojectUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        [Authorize]
        public async Task<string> Join(int surveyId)
        {

            var currentUser = await _userManager.GetUserAsync(User);
            var survey = _context.Surveys
                .Include(s => s.Allowed)
                .Include(s => s.Participants)
                .FirstOrDefault(s => s.Id == surveyId);

            if (survey == null || currentUser == null)
                return "Survey doesn't exist.";

            var allowedRoles = survey.Allowed?.Select(ar => ar.RoleName)?.ToList() ?? new List<string>();

            if (!allowedRoles.Any())
                return "Error: No allowed roles for the survey.";

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            if (!userRoles.Any(allowedRoles.Contains))
                return "You are not allowed to participate in this particular survey!";

            var isParticipant = survey.Participants?.Any(p => p.EprojectUserId == currentUser.Id) ?? false;

            if (isParticipant)
                return "Already Participated!";

            // Add the currently logged-in user to the survey's Participants collection
            survey.Participants ??= new List<SurveyEprojectUser>();
            survey.Participants.Add(new SurveyEprojectUser { EprojectUserId = currentUser.Id });

            // Save changes to the database
            await _context.SaveChangesAsync();

            return "You are part of the survey now!";
        }
    }
}
