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

            return Ok("Survey created successfully");

        }
        [Authorize]

        public async Task<IActionResult> Index()
        {
            return View();

        }
    }
}