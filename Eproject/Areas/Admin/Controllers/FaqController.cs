using Eproject.Data;
using Eproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Eproject.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Data;

namespace Eproject.Areas.Controllers.Admin
{
    public class Data
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    public class DeleteFaq
    {
        public int Id { get; set; }
    }
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class FaqController : Controller
    {
        private readonly ILogger<FaqController> _logger;
        private readonly EprojectContext _context;
        public FaqController(EprojectContext context, ILogger<FaqController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(Data data)
        {
            var entry = await _context.FaqEntries.FindAsync(data.Id);

            if (entry == null)
            {
                return NotFound(); // Handle the case where the entry is not found
            }

            entry.Question = data.Question;
            entry.Answer = data.Answer;

            try
            {
                _context.Update(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Faq"); // Update successful
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception
                return StatusCode(500, "An error occurred while updating the record.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteFaq model)
        {
            if (ModelState.IsValid)
            {
                var faqEntry = await _context.FaqEntries.FindAsync(model.Id);
                if (faqEntry == null)
                {
                    return BadRequest();
                }
                _context.FaqEntries.Remove(faqEntry);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();

        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Question,Answer")] Data data)
        {

            var faq = new FaqEntry()
            {
                Question = data.Question,
                Answer = data.Answer
            };

            await _context.AddAsync(faq);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Faq");
        }
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Json()
        {
            List<FaqEntry> faqEntries = await _context.FaqEntries.ToListAsync();
            var jsonData = new { data = faqEntries };
            return Json(jsonData);

        }

    }

}
