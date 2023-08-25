using Eproject.Data;
using Eproject.Models;
using Eproject.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Areas.Controllers.Admin
{
    [Area("Admin")]
    public class SupportController : Controller
    {
        private readonly ILogger<FaqController> _logger;
        private readonly EprojectContext _context;
        public SupportController(EprojectContext context, ILogger<FaqController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var supportInfo = await _context.SupportInformation.FirstOrDefaultAsync();
            _logger.LogCritical(supportInfo.Email);
            return View(supportInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SupportInformation information)
        {

            if (!ModelState.IsValid)
            {
                return View("Index", information); // Pass the model back to the view with validation errors
            }

            var supportInfo = await _context.SupportInformation.FirstOrDefaultAsync();

            if (supportInfo == null)
            {
                _context.SupportInformation.Add(information);
            }
            else
            {
                supportInfo.FullName = information.FullName;
                supportInfo.Company = information.Company;
                supportInfo.Email = information.Email;
                supportInfo.PhoneNumber = information.PhoneNumber;
            }

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Redirect to a success view
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "An error occurred while saving changes to the database.");
                return View("Index", information); // Pass the model back to the view with error message
            }
        }
    }
}
