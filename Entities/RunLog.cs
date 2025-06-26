namespace FitnessTrackerAPI.Entities
{
    public class RunLog
    {
        public int Id { get; set; } 
        public int WorkoutId { get; set; }
        public float DistanceInKms { get; set; }
        public string? Shoe { get; set; }

        // Navigation
        public Workout Workout { get; set; } = null!;
    }
}
