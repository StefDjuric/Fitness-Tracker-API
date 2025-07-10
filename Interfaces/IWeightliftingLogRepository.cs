using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Models;

namespace FitnessTrackerAPI.Interfaces
{
    public interface IWeightliftingLogRepository
    {
        public Task<IEnumerable<WeightliftingLogDto>> GetAllWeightliftingLogsAsync();
        public Task AddWeightliftingLogAsync(WeightliftingLog weightliftingLog);
        public void DeleteWeightliftingLog(WeightliftingLog weightlifting);
        public Task<bool> SaveChangesAsync();
    }
}
