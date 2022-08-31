using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TheBugTracker.Models;

namespace TheBugTracker.Services.Factories
{
    public class BTUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<BTUser, IdentityRole>
    {
        public BTUserClaimsPrincipalFactory(UserManager<BTUser> userManager,
                                    RoleManager<IdentityRole> roleManager,
                                    IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor) // calling 
        {

        }

    }
}
