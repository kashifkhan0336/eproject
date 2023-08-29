using AutoMapper;
using Eproject.Data;
using Eproject.Models;
using Eproject.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace Eproject.Areas.Controllers.Admin
{
    [Area("Admin")]
    public class SurveyController : Controller
    {
        private readonly ILogger<SurveyController> _logger;
        private readonly EprojectContext _context;
        private readonly UserManager<EprojectUser> _userManager;

        public SurveyController(EprojectContext context, ILogger<SurveyController> logger, IMapper mapper, UserManager<EprojectUser> userManager)
        {
            _context = context;
            _logger = logger;

            _userManager = userManager;
        }

        //Delete an entire survey
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] DeleteSurveyDto surveyDelete)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errors);
            }

            var survey = await _context.Surveys.FindAsync(surveyDelete.Id);
            if (survey == null)
            {
                return NotFound("Survey not found!");
            }
            // Delete the survey
            _context.Surveys.Remove(survey);

            await _context.SaveChangesAsync();

            return Ok("Survey deleted successfully");
        }

        public IActionResult Create()
        {
            return View("Create/Index");
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateSurveyDto surveyDto)
        {
            var validRoles = surveyDto.AllowedRoles?.Where(roleDto => roleDto.RoleName != "false").ToList();

            if (!validRoles.Any())
            {
                ModelState.AddModelError("allowedRoles", "There must a role.");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewData["Errors"] = errors;
                return View("Create/Index", surveyDto); // Pass the surveyDto back to the viewreturn View("Create/Index", ModelState.ValidationState);
            }
            if (validRoles.All(roleDto => roleDto.RoleName != "Admin"))
            {
                validRoles.Add(new AllowedRoleDto { RoleName = "Admin" });
            }
            var newSurvey = new Survey
            {
                Name = surveyDto.Name,
                Description = surveyDto.Description,
                SurveyData = surveyDto.SurveyData,
                Allowed = validRoles
                    .Select(roleDto => new AllowedRole { RoleName = roleDto.RoleName })
                    .ToList()
            };

            await _context.Surveys.AddAsync(newSurvey);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Survey");

        }
        [Authorize]

        public async Task<IActionResult> Index()
        {
            return View();

        }

        public async Task<IActionResult> Update(int surveyId)
        {
            var existingSurvey = await _context.Surveys
                .Include(s => s.Allowed)
                .FirstOrDefaultAsync(s => s.Id == surveyId);

            if (existingSurvey == null)
            {
                return NotFound("Survey not found");
            }

            var updateDto = new UpdateSurveyDto
            {
                Id = existingSurvey.Id,
                Name = existingSurvey.Name,
                Description = existingSurvey.Description,
                SurveyData = existingSurvey.SurveyData,
                AllowedRoles = existingSurvey.Allowed.Select(role => new AllowedRoleDto { RoleName = role.RoleName }).ToList(),
                IsStudentAllowed = existingSurvey.Allowed.Any(role => role.RoleName == "Student"),
                IsFacultyAllowed = existingSurvey.Allowed.Any(role => role.RoleName == "Faculty")
            };

            return View("Update/Index", updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSurveyDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                ViewData["Errors"] = errors;
                return View("Update/Index", updateDto);
            }
            var validRoles = updateDto.AllowedRoles?.Where(roleDto => roleDto.RoleName != "false").ToList();
            var existingSurvey = await _context.Surveys
                .Include(s => s.Allowed)
                .FirstOrDefaultAsync(s => s.Id == updateDto.Id);

            if (existingSurvey == null)
            {
                return NotFound("Survey not found");
            }

            existingSurvey.Name = updateDto.Name;
            existingSurvey.Description = updateDto.Description;
            existingSurvey.SurveyData = updateDto.SurveyData;

            existingSurvey.Allowed.Clear();
            if (updateDto.IsStudentAllowed)
            {
                existingSurvey.Allowed.Add(new AllowedRole { RoleName = "Student" });
                existingSurvey.Allowed.Add(new AllowedRole { RoleName = "Admin" });
            }
            if (updateDto.IsFacultyAllowed)
            {
                existingSurvey.Allowed.Add(new AllowedRole { RoleName = "Faculty" });
                existingSurvey.Allowed.Add(new AllowedRole { RoleName = "Admin" });
            }
            // Set other roles if needed

            _context.Surveys.Update(existingSurvey);
            await _context.SaveChangesAsync();
            return RedirectToAction("Update", "Survey", new { area = "Admin", surveyId = existingSurvey.Id });
        }

    }

}