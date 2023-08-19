namespace Eproject.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<AllowedRole> Allowed { get; set; } = null!;
        public ICollection<EprojectUser>? Participants { get; set; } = new List<EprojectUser>();
        //public ICollection<EprojectUser>? CompletedBy { get; set; }
        public string SurveyData { get; set; } = null!;
    }
}
