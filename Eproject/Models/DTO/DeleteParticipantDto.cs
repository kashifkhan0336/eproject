namespace Eproject.Models.DTO
{
    public class DeleteParticipantDto
    {
        public required int SurveyId { get; set; }
        public required string UserId { get; set; }
    }
}
