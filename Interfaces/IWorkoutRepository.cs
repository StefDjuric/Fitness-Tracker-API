using FitnessTrackerAPI.Entities;

namespace FitnessTrackerAPI.Interfaces
{
    public interface IWorkoutRepository
    {
        public Task<Workout?> GetWorkoutByIdAsync(int id);
        public Task<IEnumerable<Workout>> GetAllWorkoutsAsync();
        public Task<IEnumerable<Workout>> GetAllWorkoutsForUserAsync(int userId);
        public Task<IEnumerable<Workout>> GetWeightliftingWorkoutsForUserAsync(int userId);
        public Task<IEnumerable<Workout>> GetRunningWorkoutsForUserAsync(int userId);
        public Task<IEnumerable<Workout>> GetWorkoutsForUserByTypeAsync(string type, int userId);
        public Task AddWorkoutAsync(Workout workout);
        public void DeleteWorkout(Workout workout);
        public Task<int> GetUserWorkoutCountAsync(int userId);
        public Task<int> GetUserWeightliftingCountAsync(int userId);
        public Task<int> GetUserRunCountAsync(int userId);
        public Task<bool> SaveAllAsync();
    }
}
