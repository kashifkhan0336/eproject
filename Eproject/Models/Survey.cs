namespace Eproject.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public bool IsActive { get; set; } = true;
        public ICollection<AllowedRole> Allowed { get; set; } = null!;
        public ICollection<SurveyEprojectUser> Participants { get; set; } = new List<SurveyEprojectUser>();
        public ICollection<SurveyCompletion> Completions { get; set; } = new List<SurveyCompletion>();

        public string SurveyData { get; set; } = null!;

    }
}
