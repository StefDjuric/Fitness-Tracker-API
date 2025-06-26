namespace FitnessTrackerAPI.Entities
{
    public class WeightliftingLog
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }

        // Navigation
        public Workout Workout { get; set; } = null!;
        public List<Exercise> Exercises { get; set; } = [];
    }
}
