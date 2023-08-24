using System.ComponentModel.DataAnnotations;

namespace Eproject.Models.DTO
{
    public class DeleteSurveyDto
    {
        [Required]
        public int Id { get; set; }
    }
}
