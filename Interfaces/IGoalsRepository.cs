using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace FitnessTrackerAPI.Interfaces
{
    public interface IGoalsRepository
    {
        public Task SetGoalsAsync(UserGoals userGoals);
        public void RemoveGoals(UserGoals userGoals);
        public Task<IEnumerable<GoalsDto>> GetAllGoalsAsync();
        public Task<UserGoals?> GetGoalByIdAsync(int id);
        public Task<UserGoals?> GetGoalsByUserIdAsync(int userId);
        public Task<bool> SaveChangesAsync();
        public Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
