namespace Eproject.Models
{
    public class UserUpdateRole
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string RollNumber { get; set; } = null!;

        public string Class { get; set; } = null!;
        public string Specification { get; set; } = null!;

        public string Section { get; set; } = null!;
        public UserStatus Status { get; set; }
        public string Role { get; set; } = null!;

        public DateTime AdmissionDate { get; set; }

    }
}
