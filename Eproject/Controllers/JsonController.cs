using AutoMapper;
using Eproject.Data;
using Eproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Eproject.Controllers
{
    public class JsonController : Controller
    {
        private readonly ILogger<FaqController> _logger;
        private readonly EprojectContext _context;
        private readonly UserManager<EprojectUser> _userManager;

        public JsonController(EprojectContext context, ILogger<FaqController> logger, IMapper mapper, UserManager<EprojectUser> userManager)
        {
            _context = context;
            _logger = logger;

            _userManager = userManager;
        }
        public string Index()
        {
            return "Wokr!";
        }
        //Get all surveys a specific participant has participated
        public async Task<IActionResult> Survey(string participantId)
        {
            var participantSurveys = _context.Surveys
                .Where(survey => survey.Participants.Any(p => p.EprojectUserId == participantId))
                .ToList();

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var jsonSurveys = JsonConvert.SerializeObject(participantSurveys, Formatting.None, jsonSettings);

            return Content(jsonSurveys, "application/json");
        }
        [HttpGet]
        //Get all participants of an specific survey
        public async Task<IActionResult> Participants(int surveyId)
        {
            var survey = _context.Surveys
                .Include(s => s.Participants)
                .ThenInclude(p => p.EprojectUser)
                .FirstOrDefault(s => s.Id == surveyId);

            if (survey == null)
                return BadRequest();

            var participants = survey.Participants;

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var jsonParticipants = JsonConvert.SerializeObject(participants, Formatting.None, jsonSettings);

            return Content(jsonParticipants, "application/json");
        }
        //Get All Surveys
        [HttpGet]
        public async Task<IActionResult> Surveys()
        {
            var surveys = await _context.Surveys
                .Include(s => s.Allowed)
                .Include(s => s.Participants)
                .Include(s => s.Completions)
                
                .ToListAsync();

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var jsonSurveys = JsonConvert.SerializeObject(new { data = surveys}, Formatting.None, jsonSettings);

            return Content(jsonSurveys, "application/json");
        }
    }
}
