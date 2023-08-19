namespace Eproject.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Full_name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Roll_number { get; set; } = null!;

        public string Class { get; set;} = null!;
        public string Specification { get; set; } = null!;
        
        public string Section { get; set; } = null!;
        public UserStatus Status { get; set; }
        public string Role { get; set; } = null!;
        public List<string> AllRoles { get; set; } = new List<string>();
        public DateTime Admission_date { get; set; }

    }
}
