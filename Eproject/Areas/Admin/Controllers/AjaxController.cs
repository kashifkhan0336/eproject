using Eproject.Data;
using Eproject.Models;
using Eproject.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Eproject.Areas.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AjaxController : Controller
    {
        private readonly ILogger<FaqController> _logger;
        private readonly EprojectContext _context;
        private readonly UserManager<EprojectUser> _userManager;

        public AjaxController(EprojectContext context, ILogger<FaqController> logger, UserManager<EprojectUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Remove a specific participant from an specific survey
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<string> Delete(DeleteParticipantDto participantDto)
        {
            var survey = _context.Surveys
                .Include(s => s.Participants)
                .FirstOrDefault(s => s.Id == participantDto.SurveyId);

            if (survey == null)
                return "Survey doesn't exist.";

            var participantToRemove = survey.Participants
                .FirstOrDefault(p => p.EprojectUserId == participantDto.UserId);

            if (participantToRemove == null)
                return "User is not a participant in this survey.";

            survey.Participants.Remove(participantToRemove);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return "User has been removed from the survey.";
        }
    }
}
