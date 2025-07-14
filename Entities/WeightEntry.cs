namespace FitnessTrackerAPI.Entities
{
    public class WeightEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Weight { get; set; }
        public DateOnly Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;

    }
}
