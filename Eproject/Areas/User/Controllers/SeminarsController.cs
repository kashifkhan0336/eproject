using Eproject.Data;
using Eproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Eproject.Areas.User.Controllers
{
    [Area("User")]
    public class SeminarsController : Controller
    {

        private readonly ILogger<SeminarsController> _logger;
        private readonly EprojectContext _context;
        private readonly UserManager<EprojectUser> _userManager;
        public SeminarsController(EprojectContext context, ILogger<SeminarsController> logger, UserManager<EprojectUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        [Authorize(Roles="Admin,Faculty")]
        public IActionResult Create()
        {

            return View("Create/Index");
        }
        [Authorize(Roles = "Admin,Faculty")]

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]SeminarEntry seminarEntry)
        {
            ModelState.MarkFieldSkipped("EprojectUser");
            ModelState.MarkFieldSkipped("EprojectUserId");
            var _ =seminarEntry.PresentationMaterial.FileName;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the current logged-in user's ID
            var currentUser = await _userManager.GetUserAsync(User);

            // Create a new Seminar entity
            var seminar = new SeminarEntry
            {
                Title = seminarEntry.Title,
                Date = seminarEntry.Date,
                Location = seminarEntry.Location,
                NumberOfParticipants = seminarEntry.NumberOfParticipants,
                Description = seminarEntry.Description,
                Impact = seminarEntry.Impact,
                PresentationMaterialLink = string.Empty, // Initialize link to empty
            };

            if (seminarEntry.PresentationMaterial != null)
            {
                // Generate a unique filename to avoid collisions
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + seminarEntry.PresentationMaterial.FileName;
                var filePath = Path.Combine("wwwroot", "uploads", uniqueFileName); // Adjust the path as needed

                // Save the uploaded file to the server
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    seminarEntry.PresentationMaterial.CopyTo(fileStream);
                }

                // Generate the link to the uploaded file
                var fileLink = Url.Content("~/uploads/" + uniqueFileName);

                // Store the link in the Seminar entity
                seminar.PresentationMaterialLink = fileLink;
            }

            // Associate the seminar with the current logged-in user
            seminar.EprojectUserId = currentUser.Id;

            _context.SeminarEntries.Add(seminar);
            await _context.SaveChangesAsync();

            return View("Create/Index");
        }
        public IActionResult Index()
        {
            var seminarsWithUsers = _context.SeminarEntries
                .Include(seminar => seminar.EprojectUser) // Load the associated user
                .ToList();

            return View(seminarsWithUsers);
        }
    }
}
