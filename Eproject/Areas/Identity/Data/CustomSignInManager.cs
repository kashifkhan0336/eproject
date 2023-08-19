using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using Eproject.Models;

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
        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await UserManager.FindByEmailAsync(userName);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
        public override async Task<bool> CanSignInAsync(TUser user)
        {
            // Check if the user's "IsApproved" property is true
            if (user is EprojectUser appUser && appUser.Status != UserStatus.Active)
            {
                return false;
            }
            return await base.CanSignInAsync(user);
        }
    }
}