namespace FitnessTrackerAPI.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public required string Name {  get; set; }
        public float WeightInKgs { get; set; }
        public int Series { get; set; }
        public int Reps { get; set; }
        public int WeightliftingLogId { get; set; }

        // Navigation
        public WeightliftingLog WeightliftingLog { get; set; } = null!;
    }
}
