using Microsoft.AspNetCore.Identity;

namespace IUR_Backend.Settings
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
