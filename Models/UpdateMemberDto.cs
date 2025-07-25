namespace FitnessTrackerAPI.Models
{
    public class UpdateMemberDto
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? UpdatedPassword { get; set; }
        public string? CurrentPassword { get; set; }
    }
}
