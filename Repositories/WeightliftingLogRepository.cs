using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessTrackerAPI.Data;
using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Interfaces;
using FitnessTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTrackerAPI.Repositories
{
    public class WeightliftingLogRepository(DataContext context, IMapper mapper) : IWeightliftingLogRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task AddWeightliftingLogAsync(WeightliftingLog weightliftingLog)
        {
            await _context.WeightliftingLogs.AddAsync(weightliftingLog);
        }

        public void DeleteWeightliftingLog(WeightliftingLog weightliftingLog)
        {
            _context.WeightliftingLogs.Remove(weightliftingLog);
        }

        public async Task<IEnumerable<WeightliftingLogDto>> GetAllWeightliftingLogsAsync()
        {
            return await _context.WeightliftingLogs
                .ProjectTo<WeightliftingLogDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
