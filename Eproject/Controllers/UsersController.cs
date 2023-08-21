using Eproject.Areas.Identity.Pages.Account;
using Eproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Eproject.Controllers.HomeController;

namespace Eproject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<EprojectUser> _userManager;
        private readonly SignInManager<EprojectUser> _signInManager;
        private readonly IUserStore<EprojectUser> _userStore;
        private readonly IUserEmailStore<EprojectUser>? _emailStore;
        private readonly IEmailSender _emailSender;


        public UsersController(
            ILogger<HomeController> logger, 
            RoleManager<IdentityRole> roleManager, 
            UserManager<EprojectUser> userManager,
            IUserStore<EprojectUser> userStore,
            SignInManager<EprojectUser> signInManager,
            IEmailSender emailSender
            )
        {

            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        private EprojectUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<EprojectUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(EprojectUser)}'. " +
                    $"Ensure that '{nameof(EprojectUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterModel.InputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.Name = model.Name;
                user.Class = model.Class;
                user.Code = model.Code;
                user.Section = model.Section;
                user.Specification = model.Specification;
                user.AdmissionDate = DateTime.Now;
                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, model.Password);
            }

            // Return an error response
            return BadRequest();
        }

        public IActionResult Index(string userId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);

                if (user == null)
                {
                    return NotFound(new { success = false, message = "User not found." });
                }

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false, errors = result.Errors });
            }
            else
            {
                return BadRequest(new { success = false, message = "Invalid request." });
            }
        }

        public async Task<IActionResult> Manage(String UserId)
        {
            _logger.Log(LogLevel.Information, UserId);
            if(UserId == null)
            {
                return View("NotFound");
            }
            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                return View("NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.Select(role => role.Name).ToListAsync();

            var userView = new UserViewModel
            {
                Email = user.Email,
                Full_name = user.Name,
                Username = user.UserName.Split("@")[0],
                Roll_number = user.Code,
                Section = user.Section,
                Specification = user.Specification,
                UserId = user.Id,
                Class = user.Class,
                Role = String.Join("", roles),
                Status = user.Status,
                Admission_date = user.AdmissionDate,
                AllRoles = allRoles
            };
            return View(userView);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateData(UserUpdateRole userView)
        {

            var user = await _userManager.FindByIdAsync(userView.UserId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            user.Email = userView.Email;
            user.Name = userView.FullName;
            user.UserName = userView.UserName; // Reconstruct the email with updated username
            user.Code = userView.RollNumber;
            user.Section = userView.Section;
            user.Specification = userView.Specification;
            user.Class = userView.Class;
            user.Status = userView.Status;
            user.AdmissionDate = userView.AdmissionDate;

            // Update roles
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, userView.Role);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("User update failed.");
            }

            return RedirectToAction("Manage", new { userId = userView.UserId });
        }
        [HttpGet]
        public async Task<IActionResult> Json()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles1 = new List<UserViewModel>();
            var allRoles = await _roleManager.Roles.Select(role => role.Name).ToListAsync();
            foreach ((EprojectUser user, int index) in users.Select((item, index) => (item, index)))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userWithRoles = new UserViewModel
                {
                    Id = index,
                    Email = user.Email,
                    Full_name = user.Name,
                    Username = user.UserName.Split("@")[0],
                    Roll_number = user.Code,
                    Section = user.Section,
                    Specification = user.Specification,
                    UserId = user.Id,
                    Class = user.Class,
                    Role = String.Join("", roles),
                    Status = user.Status,
                    Admission_date = user.AdmissionDate,
                    AllRoles = allRoles

                };
                usersWithRoles1.Add(userWithRoles);
            }
            var jsonData = new { data = usersWithRoles1 };
            return Json(jsonData);
        }
        

    }
}
