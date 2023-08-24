using Eproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Areas.Controllers.Admin
{

    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<EprojectUser> _userManager;
        private readonly SignInManager<EprojectUser> _signInManager;
        private readonly IUserStore<EprojectUser> _userStore;
        private readonly IUserEmailStore<EprojectUser>? _emailStore;
        private readonly IEmailSender _emailSender;


        public AdminController(
            ILogger<AdminController> logger,
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
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View(@"Faq/Index");
        }

        //public IActionResult Surveys()
        //{
        //    return View("Survey/Index");
        //}


    }

}
