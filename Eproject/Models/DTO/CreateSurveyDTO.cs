using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eproject.Models.DTO
{
    public class CreateSurveyDto
    {
        public CreateSurveyDto()
        {
            AllowedRoles = new List<AllowedRoleDto>
            {
                new AllowedRoleDto { RoleName = "Student" },
                new AllowedRoleDto { RoleName = "Faculty" }
            };
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string SurveyData { get; set; }
        [Required]
        public List<AllowedRoleDto> AllowedRoles { get; set; }
    }

    public class AllowedRoleDto
    {
        public string RoleName { get; set; }
    }

}
