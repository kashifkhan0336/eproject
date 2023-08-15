using Eproject.Areas.Identity.Data;
using Eproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Eproject.Controllers.HomeController;

namespace Eproject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<EprojectUser> _userManager;

        public AdminController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<EprojectUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
        }
        [HttpPost]
        public IActionResult UpdateApproval([FromBody] UpdateApprovalViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Load the user from the database based on model.UserId
                var user = _userManager.FindByIdAsync(model.UserId).Result;

                if (user != null)
                {
                    user.isApproved = model.IsApproved;
                    var result = _userManager.UpdateAsync(user).Result;

                    if (result.Succeeded)
                    {
                        // Return a success response
                        return Ok();
                    }
                }
            }

            // Return an error response
            return BadRequest();
        }
        
        public async Task<IActionResult> Manage(string userId)
        {
            _logger.Log(LogLevel.Information, userId);
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles1 = new List<UserViewModel>();

            foreach (EprojectUser user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userWithRoles = new UserViewModel
                {
                    Email = user.Email,
                    UserName = user.Name,
                    Code = user.Code,
                    Section = user.Section,
                    Specification = user.Specification,
                    Class = user.Class,
                    Roles = roles,
                    IsApproved = user.isApproved,
                    UserId = user.Id

                };
                usersWithRoles1.Add(userWithRoles);
            }

            return View(usersWithRoles1);
        }

    }
}
