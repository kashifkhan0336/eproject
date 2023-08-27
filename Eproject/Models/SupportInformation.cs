using System.ComponentModel.DataAnnotations;

namespace Eproject.Models
{
    public class SupportInformation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string Location { get; set; }

        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string PhoneNumber { get; set; }
    }
}
