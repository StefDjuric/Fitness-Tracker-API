using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace FitnessTrackerAPI.Interfaces
{
    public interface IWeeklyProgressRepository
    {
        public Task<IEnumerable<WeeklyProgressDto>> GetAllWeeklyProgressAsync();
        public Task<WeeklyProgress?> GetWeeklyProgressByIdAsync(int id);
        public Task<WeeklyProgress?> GetWeeklyProgressByUserIdAsync(int userId);
        public Task InitializeWeeklyProgressAsync(WeeklyProgress weeklyProgress);
        public void RemoveWeeklyProgress(WeeklyProgress weeklyProgress);
        public void Update(WeeklyProgress weeklyProgress);
        public Task<bool> SaveChangesAsync();
        public Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
