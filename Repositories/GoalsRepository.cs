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
    public class GoalsRepository(DataContext context, IMapper mapper) : IGoalsRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<IEnumerable<GoalsDto>> GetAllGoalsAsync()
        {
            return await _context.UserGoals
                .ProjectTo<GoalsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<UserGoals?> GetGoalByIdAsync(int id)
        {
            return await _context.UserGoals
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<UserGoals?> GetGoalsByUserIdAsync(int userId)
        {
            return await _context.UserGoals
                .Where(x => x.UserId == userId)
                .SingleOrDefaultAsync();
        }

        public void RemoveGoals(UserGoals userGoals)
        {
            _context.Remove(userGoals);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task SetGoalsAsync(UserGoals userGoals)
        {
             await _context.AddAsync(userGoals);
        }
    }
}
