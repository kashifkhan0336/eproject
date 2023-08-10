using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Eproject.Areas.Identity.Data;

// Add profile data for application users by adding properties to the EprojectUser class
public class EprojectUser : IdentityUser
{
    [ProtectedPersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string? Name { get; set; }

    [ProtectedPersonalData]
    public DateTime AdmissionDate { get; set; } = DateTime.Now;

    public bool isApproved { get; set; }
}

