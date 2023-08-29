using Microsoft.AspNetCore.Mvc;

namespace Eproject.Areas.User.Controllers
{
    [Area("User")]
    public class ErrorController : Controller
    {
        [Route("/Error/{code:int}")]
        public IActionResult Index(int code)
        {
            switch (code)
            {
                case 405:
                    ViewData["ErrorMessage"] = "Bad Request - Invalid Method";
                    ViewData["ErrorCode"] = code;
                    return View();

                case 404:
                    ViewData["ErrorMessage"] = "Not Found - Resource not available";
                    ViewData["ErrorCode"] = code;
                    return View();

                // Add more cases for other error codes if needed

                default:
                    ViewData["ErrorMessage"] = $"Error occurred. The ErrorCode is: {code}";
                    return View();
            }
        }
    }
}
