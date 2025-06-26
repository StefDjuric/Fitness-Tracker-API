using Microsoft.AspNetCore.Identity;

namespace FitnessTrackerAPI.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
