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


namespace Eproject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SurveyController : Controller
    {
        private readonly ILogger<FaqController> _logger;
        private readonly EprojectContext _context;
        private readonly UserManager<EprojectUser> _userManager;

        public SurveyController(EprojectContext context, ILogger<FaqController> logger, IMapper mapper, UserManager<EprojectUser> userManager)
        {
            _context = context;
            _logger = logger;

            _userManager = userManager;
        }

        //Delete an entire survey
        public async Task<string> Delete(int Id)
        {
            var survey = await _context.Surveys
                .Include(s => s.Participants)
                .Include(s => s.Allowed)
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (survey == null)
            {
                return "No Survey";
            }

            // Remove participants and allowed roles

            // Delete the survey
            _context.Surveys.Remove(survey);

            await _context.SaveChangesAsync();

            return "Survey deleted successfully";
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateSurveyDto surveyDto)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errors);
            }

            // Filter out roles with the value "false"
            var validRoles = surveyDto.AllowedRoles?.Where(roleDto => roleDto.RoleName != "false").ToList();

            var allowedRoles = validRoles ?? new List<AllowedRoleDto>
            {
                new() { RoleName = "Student" },
                new() { RoleName = "Faculty" }
            };

            var newSurvey = new Survey
            {
                Name = surveyDto.Name,
                Description = surveyDto.Description,
                SurveyData = surveyDto.SurveyData,
                Allowed = allowedRoles
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