using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessTrackerAPI.Data;
using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Interfaces;
using FitnessTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FitnessTrackerAPI.Repositories
{
    public class WeeklyProgressRepository(DataContext context, IMapper mapper) : IWeeklyProgressRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<IEnumerable<WeeklyProgressDto>> GetAllWeeklyProgressAsync()
        {
            return await _context.WeeklyProgress
                .ProjectTo<WeeklyProgressDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<WeeklyProgress?> GetWeeklyProgressByIdAsync(int id)
        {
            return await _context.WeeklyProgress
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WeeklyProgress?> GetWeeklyProgressByUserIdAsync(int userId)
        {
            return await _context.WeeklyProgress
                .Where(x => x.UserId == userId)
                .SingleOrDefaultAsync();
        }

        public async Task InitializeWeeklyProgressAsync(WeeklyProgress weeklyProgress)
        {
            await _context.WeeklyProgress.AddAsync(weeklyProgress);
        }

        public void RemoveWeeklyProgress(WeeklyProgress weeklyProgress)
        {
            _context.WeeklyProgress.Remove(weeklyProgress);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
