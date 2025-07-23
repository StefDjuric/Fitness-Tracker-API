using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Models;

namespace FitnessTrackerAPI.Interfaces
{
    public interface IWeightEntryRepository
    {
        public Task<IEnumerable<WeightEntryDto>> GetAllWeightEntriesAsync();
        public Task<IEnumerable<WeightEntryDto?>> GetWeightEntriesByUserIdAsync(int userId, int daysBack);
        public Task<WeightEntry?> GetWeightEntryByIdAsync(int id);
        public Task AddWeightEntryAsync(WeightEntry weightEntry);
        public void RemoveWeightEntry(WeightEntry weightEntry);
        public Task<bool> SaveAllAsync();
    }
}
