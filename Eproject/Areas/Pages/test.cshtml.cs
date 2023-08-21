using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eproject.Pages
{
    public class testModel : PageModel
    {
        [BindProperty]
        public int[] SelectedTags { get; set; }
        public SelectList TagOptions { get; set; }
        public void OnGet()
        {
            var roleOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Student", Text = "Student" },
                new SelectListItem { Value = "Faculty", Text = "Faculty" },
                // Add more options if needed
            };

            TagOptions = new SelectList(roleOptions, "Value", "Text");
        }
    }
}
