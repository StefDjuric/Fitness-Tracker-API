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
                .AsNoTracking()
                .Include(w => w.RunLog)
                .Include(w => w.WeightliftingLog)
                .ThenInclude(w => w.Exercises)
                .OrderByDescending(w => w.WorkoutDate)
                .ToListAsync();
                
        }

        public async Task<IEnumerable<Workout>> GetAllWorkoutsForUserAsync(int userId)
        {
            return await _context.Workouts
                .AsNoTracking()
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
                .AsNoTracking()
                .Where(w => w.UserId == userId)
                .Include(w => w.RunLog)
                .OrderByDescending(w => w.WorkoutDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Workout>> GetWeightliftingWorkoutsForUserAsync(int userId)
        {
            return await _context.Workouts
                .AsNoTracking()
                .Where(w => w.UserId == userId)
                .Include(w => w.WeightliftingLog)
                .ThenInclude(w => w.Exercises)
                .OrderByDescending(w => w.WorkoutDate)
                .ToListAsync();
        }

        public async Task<Workout?> GetWorkoutByIdAsync(int id)
        {
            return await _context.Workouts.FirstOrDefaultAsync(w => w.Id == id);
        }


        public async Task<IEnumerable<Workout>> GetWorkoutsForUserByTypeAsync(string type, int userId)
        {
            return await _context.Workouts
                .AsNoTracking()
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
