using FitnessTrackerAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerAPI.Models
{
    public class WorkoutDto
    {
        public int? Id { get; set; }
        [Required]
        public required string Type { get; set; }
        [Required]
        public required float DurationMin { get; set; }
        public  DateTime? WorkoutDate { get; set; }
        public float? Calories { get; set; }
        public string? Notes { get; set; }
        public RunLogDto? RunLog { get; set; }
        public WeightliftingLogDto? WeightliftingLog { get; set; }
    }
}
