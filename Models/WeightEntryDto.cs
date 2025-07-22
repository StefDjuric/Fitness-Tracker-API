namespace FitnessTrackerAPI.Models
{
    public class WeightEntryDto
    {
        public required float Weight { get; set; }
        public required DateOnly Date { get; set; }

    }
}
