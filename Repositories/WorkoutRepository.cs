using FitnessTrackerAPI.Data;
using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTrackerAPI.Repositories
{
    public class WorkoutRepository(DataContext context) : IWorkoutRepository
    {
        private readonly DataContext _context = context;

        public async Task AddWorkoutAsync(Workout workout)
        {
            await _context.Workouts.AddAsync(workout);
        }

        public void DeleteWorkout(Workout workout)
        {
            _context.Workouts.Remove(workout);
        }

        public async Task<IEnumerable<Workout>> GetAllWorkoutsAsync()
        {
            return await _context.Workouts
                .Include(w => w.RunLog)
                .Include(w => w.WeightliftingLog)
                .ThenInclude(w => w.Exercises)
                .OrderByDescending(w => w.WorkoutDate)
                .ToListAsync();
                
        }

        public async Task<IEnumerable<Workout>> GetAllWorkoutsForUserAsync(int userId)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId)
                .Include(w => w.WeightliftingLog)
                .ThenInclude(wl => wl.Exercises)
                .Include(w => w.RunLog)
                .OrderByDescending(w => w.WorkoutDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Workout>> GetRunningWorkoutsForUserAsync(int userId)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId)
                .Include(w => w.RunLog)
                .OrderByDescending(w => w.WorkoutDate)
                .ToListAsync();
        }

        public async Task<int> GetUserRunCountAsync(int userId)
        {
            return await _context.RunLogs
                .Where(x => x.Workout.UserId == userId)
                .CountAsync();
        }

        public async Task<int> GetUserWeightliftingCountAsync(int userId)
        {
            return await _context.WeightliftingLogs
                .Where(x => x.Workout.UserId == userId)
                .CountAsync();
        }

        public async Task<int> GetUserWorkoutCountAsync(int userId)
        {
            return await _context.Workouts
                .Where(x => x.UserId == userId)
                .CountAsync();
        }

        public async Task<IEnumerable<Workout>> GetWeightliftingWorkoutsForUserAsync(int userId)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId)
                .Include(w => w.WeightliftingLog)
                .ThenInclude(w => w.Exercises)
                .OrderByDescending(w => w.WorkoutDate)
                .ToListAsync();
        }

        public async Task<Workout?> GetWorkoutByIdAsync(int id)
        {
            return await _context.Workouts
                .Include(x => x.WeightliftingLog)
                .ThenInclude(x => x.Exercises)
                .Include(x => x.RunLog)
                .FirstOrDefaultAsync(w => w.Id == id);
        }


        public async Task<IEnumerable<Workout>> GetWorkoutsForUserByTypeAsync(string type, int userId)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId && w.Type == type)
                .OrderByDescending(w => w.WorkoutDate)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
