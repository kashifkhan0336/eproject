﻿using Eproject.Areas.Controllers.Admin;
using Eproject.Data;
using Eproject.Models;
using Eproject.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eproject.Areas.User.Controllers
{
    [Area("User")]
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
        //public IActionResult Index()
        //{
        //    return View();
        //}
        
        [Authorize]
        [HttpPost]
        public async Task<string> Complete([FromBody]SurveyCompletionDto completionDto)
        {
            if (!ModelState.IsValid)
            {
                return "Bad request!";
            }
            var currentUser = await _userManager.GetUserAsync(User);
            var survey = await _context.Surveys
                .Include(s => s.Allowed)
                .FirstOrDefaultAsync(s => s.Id == completionDto.surveyId);

            if (survey == null || currentUser == null)
            {
                return "Survey doesn't exist.";
            }

            var allowedRoles = survey.Allowed?.Select(ar => ar.RoleName)?.ToList() ?? new List<string>();

            if (!allowedRoles.Any())
            {
                return "Error: No allowed roles for the survey.";
            }

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            if (!userRoles.Any(allowedRoles.Contains))
            {
                return "You are not allowed to participate in this particular survey!";
            }

            // Check if the user has already completed this survey
            var existingCompletion = await _context.SurveyCompletions
                .FirstOrDefaultAsync(sc => sc.SurveyId == completionDto.surveyId && sc.EprojectUserId == currentUser.Id);

            if (existingCompletion != null)
            {
                return "You have already completed this survey.";
            }

            var completion = new SurveyCompletion
            {
                SurveyId = completionDto.surveyId,
                EprojectUserId = currentUser.Id,
                Points = completionDto.Points
            };

            _context.SurveyCompletions.Add(completion); // Add completion to context
            await _context.SaveChangesAsync();

            return "Survey completed and completion recorded successfully";
        }
        [Authorize]
        public async Task<IActionResult> Join(int surveyId)
        {
            ViewData["surveyId"] = surveyId;
            var currentUser = await _userManager.GetUserAsync(User);
            var survey = _context.Surveys
                .Include(s => s.Allowed)
                .Include(s => s.Participants)
                .FirstOrDefault(s => s.Id == surveyId);

            if (survey == null || currentUser == null)
                return NotFound();

            var allowedRoles = survey.Allowed?.Select(ar => ar.RoleName)?.ToList() ?? new List<string>();

            if (!allowedRoles.Any())
                return Unauthorized();

            var userRoles = await _userManager.GetRolesAsync(currentUser);

            if (!userRoles.Any(allowedRoles.Contains))
                return View("Errors/NotAllowed"); ;

            var isParticipant = survey.Participants?.Any(p => p.EprojectUserId == currentUser.Id) ?? false;

            var existingCompletion = await _context.SurveyCompletions
                .FirstOrDefaultAsync(sc => sc.SurveyId == surveyId && sc.EprojectUserId == currentUser.Id);

            if (existingCompletion != null)
            {
                return View("Errors/AlreadyCompleted");
            }
            if (isParticipant)
                return View("Index", survey);

            // Add the currently logged-in user to the survey's Participants collection
            survey.Participants ??= new List<SurveyEprojectUser>();
            survey.Participants.Add(new SurveyEprojectUser { EprojectUserId = currentUser.Id });

            // Save changes to the database
            await _context.SaveChangesAsync();
            ViewData["SurveyData"] = survey.SurveyData;
            return View("Index", survey);
        }
    }
}
