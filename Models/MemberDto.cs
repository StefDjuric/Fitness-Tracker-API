﻿namespace FitnessTrackerAPI.Models
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Avatar { get; set; }

    }
}
