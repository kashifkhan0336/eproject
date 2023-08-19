namespace Eproject.Models
{
    public class AllowedRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
    }
}
