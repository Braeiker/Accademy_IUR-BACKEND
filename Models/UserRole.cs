using IUR_Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace IUR_Backend.Settings
{
    public class UserRole : IdentityUserRole<string>
    {
        public Role Role { get; set; }
        public ApplicationUser User { get; set; }

    }
}
