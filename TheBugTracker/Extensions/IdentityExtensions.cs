using System.Security.Claims;
using System.Security.Principal;

namespace TheBugTracker.Extensions
{
    public static class IdentityExtensions // when its static, only one instance, access methods and properties by just calling class name
    {
        public static int? GetCompanyId(this IIdentity identity) // we are extending the identity interface, extending with this method
        {
            Claim claim = ((ClaimsIdentity)identity).FindFirst("CompanyId"); // claims identity implements IIdentity so you can cast it

            
            return (claim != null) ? int.Parse(claim.Value) : null; // change companyId from string to int, or if empty return null

        }

    }
}
