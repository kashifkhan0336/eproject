using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class SeminarEntry
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfParticipants { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public string Impact { get; set; } // New property for Impact
        [NotMapped]
        public IFormFile PresentationMaterial { get; set; }
        public string? PresentationMaterialLink { get; set; }

        // Foreign key property
        public string? EprojectUserId { get; set; }

        // Navigation property representing the associated EprojectUser
        public EprojectUser? EprojectUser { get; set; }
    }
}
