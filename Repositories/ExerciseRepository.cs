using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessTrackerAPI.Data;
using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Interfaces;
using FitnessTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTrackerAPI.Repositories
{
    public class ExerciseRepository(DataContext context, IMapper mapper) : IExerciseRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task AddExerciseAsync(Exercise exercise)
        {
            await _context.Exercises.AddAsync(exercise);
        }

        public void DeleteExercise(Exercise exercise)
        {
            _context.Exercises.Remove(exercise);
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllExercisesAsync()
        {
            return await _context.Exercises
                .AsNoTracking()
                .ProjectTo<ExerciseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ExerciseDto?> GetExerciseByIdAsync(int exerciseId)
        {
            return await _context.Exercises
                .AsNoTracking()
                .Where(x => x.Id == exerciseId)
                .ProjectTo<ExerciseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ExerciseDto>> GetExercisesForSessionByWeightliftingIdAsync(int weightliftingId)
        {
            return await _context.Exercises
                .Where(x => x.WeightliftingLogId == weightliftingId)
                .AsNoTracking()
                .ProjectTo<ExerciseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
