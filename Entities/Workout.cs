namespace FitnessTrackerAPI.Entities
{
    public class Workout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Type { get; set; }
        public required float DurationMin { get; set; }
        public DateTime? WorkoutDate { get; set; }
        public float? Calories { get; set; }
        public string? Notes { get; set; }

        // Navigation
        public RunLog? RunLog { get; set; } 
        public WeightliftingLog? WeightliftingLog { get; set; }
    }
}
