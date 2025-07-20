using Microsoft.AspNetCore.Identity;

namespace FitnessTrackerAPI.Entities
{
    public class User : IdentityUser<int>
    {
        public required string FullName { get; set; }
        public string? Avatar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
