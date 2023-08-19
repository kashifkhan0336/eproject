using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Eproject.Models;

// Add profile data for application users by adding properties to the EprojectUser class
public class EprojectUser : IdentityUser
{
    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string? Name { get; set; }
    [Required]
    [MaxLength(10)]
    public string? Code { get; set; }

    [Required]
    [RegularExpression(@"^\d+$", ErrorMessage = "Class must be a numeric value.")]
    public string? Class { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Specification { get; set; }

    [Required]
    [MaxLength(1)]
    public string? Section { get; set; }


    [Required]
    public DateTime AdmissionDate { get; set; } = DateTime.Now;

    public UserStatus Status { get; set; } = UserStatus.Pending;

    public Survey? Survey { get; set; }
}

public enum UserStatus
{
    Active,
    Inactive,
    Pending
}