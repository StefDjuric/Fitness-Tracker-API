namespace FitnessTrackerAPI.Models
{
    public class GoalsDto
    {
        public required int WorkoutsGoalInWeek { get; set; } = 3;
        public required float WaterGoalInLiters { get; set; } = 3;
        public required float WeightGoal { get; set; }
        public required int MealsEatenGoal { get; set; } = 3;
    }
}
