namespace FitnessTrackerAPI.Entities
{
    public class WeeklyProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WorkoutsDone { get; set; } = 0;
        public float WaterConsumed { get; set; } = 0;
        public int MealsEaten { get; set; } = 0;
        public int WeeklyWorkoutStreak { get; set; } = 0;
        public DateOnly WeekStartDate { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public User User { get; set; } = null!;
    }
}
