using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Models;

namespace FitnessTrackerAPI.Interfaces
{
    public interface IExerciseRepository
    {
        public Task<ExerciseDto?> GetExerciseByIdAsync(int exerciseId);
        public Task<IEnumerable<ExerciseDto>> GetAllExercisesAsync();
        public Task<IEnumerable<ExerciseDto>> GetExercisesForSessionByWeightliftingIdAsync(int weightliftingId);
        public Task AddExerciseAsync(Exercise exercise);
        public void DeleteExercise(Exercise exercise);
        public Task<bool> SaveChangesAsync();
    }
}
