namespace Eproject.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Class { get; set;} = null!;
        public string Specification { get; set; } = null!;
        
        public string Section { get; set; } = null!;
        public bool IsApproved { get; set; }
        public IEnumerable<string> Roles { get; set; }

    }
}
