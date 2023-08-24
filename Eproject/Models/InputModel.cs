// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Eproject.Areas.Identity.Pages.Account
{
    public partial class RegisterModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Full name")]
            public string Name { get; set; }

            [Required]
            [MaxLength(6)]
            [DataType(DataType.Text)]
            [Display(Name = "Roll/Employee Number")]
            public string Code { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Class")]
            public string Class { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [MaxLength(20)]
            [Display(Name = "Specification")]
            public string Specification { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Section")]
            [MaxLength(1)]
            public string Section { get; set; }

            [Required]
            [Display(Name = "Admission Date")]
            [DataType(DataType.Date)]
            public DateTime AdmissionDate { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } 

            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
