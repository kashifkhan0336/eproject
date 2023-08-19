using Eproject.Data;
using Eproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ILogger<FaqController> _logger;
        private readonly EprojectContext _context;
        private readonly UserManager<EprojectUser> _userManager;
        public SurveyController(EprojectContext context, ILogger<FaqController> logger, UserManager<EprojectUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        public async Task<String> Delete(string Id)
        {
            var surveyId = 2; // Replace with the actual survey ID
            var survey = _context.Surveys
                                .Include(s => s.Participants) // Include the Participants navigation property
                                .FirstOrDefault(s => s.Id == surveyId);
            var currentUser = _userManager.FindByIdAsync(Id).Result;
            if (survey != null && currentUser != null)
            {
                var participantToRemove = survey.Participants.FirstOrDefault(p => p.Id == currentUser.Id);

                if (participantToRemove != null)
                {
                    // Remove the user from the Participants collection
                    survey.Participants.Remove(participantToRemove);

                    // Save changes to the database
                    _context.SaveChanges();
                    return "User removed!";
                }
                else
                {
                    return "that user is not a participant";
                    // Handle the case where the user is not a participant
                }
            }
            return "error";
        }
        [Authorize]
        public async Task<string> Index()
        {
            var surveyId = 2;

            var currentUser = await _userManager.GetUserAsync(User);
            var survey = _context.Surveys
                    .Include(s => s.Allowed) // Include the AllowedRoles navigation property
                    .FirstOrDefault(s => s.Id == surveyId);

            if (survey != null && currentUser != null)
            {
                // Retrieve the allowed roles for the survey
                var allowedRoles = survey.Allowed?.Select(ar => ar.RoleName)?.ToList() ?? new List<string>();

                if (allowedRoles != null && allowedRoles.Any())
                {
                    // Check if the user has any of the allowed roles before allowing participation
                    var userRoles = await _userManager.GetRolesAsync(currentUser);

                    if (userRoles != null && userRoles.Any(role => allowedRoles.Contains(role)))
                    {
                        // Check if the user is already a participant
                        if (!survey.Participants.Any(p => p.Id == currentUser.Id))
                        {
                            // Add the currently logged-in user to the survey's Participants collection
                            if (survey.Participants == null)
                            {
                                survey.Participants = new List<EprojectUser> { currentUser };
                            }
                            else
                            {
                                survey.Participants.Add(currentUser);
                            }

                            // Save changes to the database
                            _context.SaveChanges();
                            return "You are part of the survey now!";
                        }
                        // If the user is already a participant, handle this scenario
                        else
                        {
                            return "Already Participated!";
                            // Handle the case where the user is already a participant
                        }
                    }
                    else
                    {
                        return "You are not allowed to participate in this particular survey!";
                        // Handle the case where the user doesn't have any of the allowed roles
                    }
                }
                else
                {
                    return "error";
                    // Handle the case where allowedRoles is null or empty
                }
            }
            return "error";
        }
    }
}