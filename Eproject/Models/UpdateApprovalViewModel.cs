namespace Eproject.Controllers
{
    public partial class HomeController
    {
        public class UpdateApprovalViewModel
        {
            public string UserId { get; set; }
            public bool IsApproved { get; set; }
        }
    }
}