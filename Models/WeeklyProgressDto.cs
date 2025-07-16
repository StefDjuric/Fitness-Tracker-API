namespace FitnessTrackerAPI.Models
{
    public class WeeklyProgressDto
    {
        public int WorkoutsDone { get; set; } = 0;
        public float WaterConsumed { get; set; } = 0;
        public int MealsEaten { get; set; } = 0;
        public DateOnly WeekStartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
