namespace Eproject.Models
{
    public class SurveyEprojectUser
    {
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public string EprojectUserId { get; set; }
        public EprojectUser EprojectUser { get; set; }
    }
}