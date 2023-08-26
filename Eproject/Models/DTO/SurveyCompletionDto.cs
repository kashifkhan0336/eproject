using System.ComponentModel.DataAnnotations;

namespace Eproject.Models.DTO
{
    public class SurveyCompletionDto
    {
        [Required]
        public int surveyId { get; set; }

        [Required]
        public int Points {get; set; }
    }
}
