using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Security.Claims;
using TheBugTracker.Models;

namespace TheBugTracker.Services.Factories
{
    public class BTUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<BTUser, IdentityRole>
    {
        public BTUserClaimsPrincipalFactory(UserManager<BTUser> userManager,
                                    RoleManager<IdentityRole> roleManager,
                                    IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor) // calling the parent constructor
        {
        }
        // User claims is information that will persist throughout users entire current session
        // In this case, company is needed at all times, and rather than reach back to DB constantly, we use user claims
        // Company will not change during session, or at all

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(BTUser user)
        {
            ClaimsIdentity identity = await base.GenerateClaimsAsync(user);
            // adding company id claim to BTUser
            // use this to access logged in users company id
            identity.AddClaim(new Claim("CompanyId", user.CompanyId.ToString())); // string literal name doesnt matter, but need to reference later
            return identity;
        }

    }
}
