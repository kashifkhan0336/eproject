using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eproject.Areas.Identity.Data
{
    public class CustomSignInManager<TUser> : SignInManager<TUser> where TUser : class
    {
        public CustomSignInManager(UserManager<TUser> userManager,
                                   IHttpContextAccessor contextAccessor,
                                   IUserClaimsPrincipalFactory<TUser> claimsFactory,
                                   IOptions<IdentityOptions> optionsAccessor,
                                   ILogger<SignInManager<TUser>> logger,
                                   IAuthenticationSchemeProvider schemes,
                                   IUserConfirmation<TUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task<bool> CanSignInAsync(TUser user)
        {
            // Check if the user's "IsApproved" property is true
            if (user is EprojectUser appUser && !appUser.isApproved)
            {
                return false;
            }

            return await base.CanSignInAsync(user);
        }
    }
}