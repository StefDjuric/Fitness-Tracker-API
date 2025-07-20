namespace FitnessTrackerAPI.Models
{
    public class WeeklyProgressUpdateDto
    {
        public int? WorkoutsDone { get; set; }
        public float? WaterConsumed { get; set; }
        public int? MealsEaten { get; set; }
        public int? WeeklyWorkoutStreak { get; set; }

    }
}
