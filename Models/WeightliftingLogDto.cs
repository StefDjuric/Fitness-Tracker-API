using FitnessTrackerAPI.Entities;

namespace FitnessTrackerAPI.Models
{
    public class WeightliftingLogDto
    {
        public required List<ExerciseDto> Exercises { get; set; }
    }
}
