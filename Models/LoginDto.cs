using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerAPI.Models
{
    public class LoginDto
    {
        [Required]
        public required string EmailOrUsername { get; set; } = string.Empty;
        [Required]
        public required string Password { get; set; } = string.Empty;
    }
}
