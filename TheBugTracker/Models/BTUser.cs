using Microsoft.AspNetCore.Identity;

namespace TheBugTracker.Models
{
    public class BTUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
