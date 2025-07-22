using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessTrackerAPI.Data;
using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Interfaces;
using FitnessTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTrackerAPI.Repositories
{
    public class WeightEntryRepository(DataContext context, IMapper mapper) : IWeightEntryRepository
    {
        private readonly DataContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task AddWeightEntryAsync(WeightEntry weightEntry)
        {
            await _context.WeightEntries.AddAsync(weightEntry);
        }

        public async Task<IEnumerable<WeightEntryDto>> GetAllWeightEntriesAsync()
        {
            return await _context.WeightEntries
                .ProjectTo<WeightEntryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<WeightEntry?> GetWeightEntryByIdAsync(int id)
        {
            return await _context.WeightEntries
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<WeightEntryDto?>> GetWeightEntriesByUserIdAsync(int userId)
        {
            return await _context.WeightEntries
                .Where(x => x.UserId == userId)
                .ProjectTo<WeightEntryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void RemoveWeightEntry(WeightEntry weightEntry)
        {
            _context.WeightEntries.Remove(weightEntry);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
