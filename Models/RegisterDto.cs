using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerAPI.Models
{
    public class RegisterDto
    {
        [Required]
        public required string Email{ get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public required string UserName { get; set; } = string.Empty;
        [Required]
        public required string FullName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public required string Password { get; set; } = string.Empty;
    }
}
