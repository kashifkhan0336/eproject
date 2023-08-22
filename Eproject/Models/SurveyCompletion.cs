namespace Eproject.Models
{
    public class SurveyCompletion
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string EprojectUserId { get; set; }
        public int Points { get; set; }

        public Survey Survey { get; set; }
        public EprojectUser User { get; set; }
    }
}
