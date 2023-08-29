using System.ComponentModel.DataAnnotations;

namespace Eproject.Models.DTO
{
    public class UpdateSurveyDto
    {
        public UpdateSurveyDto()
        {
            AllowedRoles = new List<AllowedRoleDto>
            {
                new AllowedRoleDto { RoleName = "Student" },
                new AllowedRoleDto { RoleName = "Faculty" }
            };
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SurveyData { get; set; }
        [Required]
        public List<AllowedRoleDto> AllowedRoles { get; set; }


        public bool IsStudentAllowed { get; set; }
        public bool IsFacultyAllowed { get; set; }
    }

}