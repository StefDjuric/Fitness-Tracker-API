using System.ComponentModel.DataAnnotations;

namespace FitnessTrackerAPI.Models
{
    public class ExerciseDto
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required float WeightInKgs { get; set; }
        [Required]
        public required int Series { get; set; }
        [Required]
        public  required int Reps { get; set; }
    }
}
