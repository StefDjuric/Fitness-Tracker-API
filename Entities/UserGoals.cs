namespace FitnessTrackerAPI.Entities
{
    public class UserGoals
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WorkoutsGoalInWeek { get; set; } = 3;
        public float WaterGoalInLiters { get; set; } = 3;
        public float WeightGoal { get; set; }
        public int MealsEatenGoal { get; set; } = 3;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public User User { get; set; } = null!;
    }
}
