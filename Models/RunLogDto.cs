using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerAPI.Models
{
    public class RunLogDto
    {
        [Required]
        public required float DistanceInKms { get; set; }
        public string? Shoe { get; set; }
    }
}
