using AutoMapper;
using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Interfaces;
using FitnessTrackerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeightEntriesController(IWeightEntryRepository weightEntryRepository, IMapper mapper) : ControllerBase
    {
        private readonly IWeightEntryRepository _weightEntryRepository = weightEntryRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet] // api/weightentries
        public async Task<ActionResult<List<WeightEntryDto>>> GetAllWeightEntries()
        {
            var weightEntries = await _weightEntryRepository.GetAllWeightEntriesAsync();

            return Ok(weightEntries);
        }

        [HttpGet("{id:int}", Name = "GetWeightEntryById")] // api/weightentries/id
        public async Task<ActionResult<WeightEntryDto?>> GetWeightEntryById(int id)
        {
            var weightEntry = await _weightEntryRepository.GetWeightEntryByIdAsync(id);

            if (weightEntry == null) return NotFound("No weight entry found with that id.");

            return Ok(_mapper.Map<WeightEntryDto>(weightEntry));
        }

        [HttpGet("for-user")] // api/weightentries/for-user
        public async Task<ActionResult<List<WeightEntryDto>>> GetWeightEntriesForUser([FromQuery]int days)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var userWeightEntries = await _weightEntryRepository.GetWeightEntriesByUserIdAsync(userId, days);

            return Ok(userWeightEntries);
        }

        [HttpPost("add")] // api/weightentries/add
        public async Task<ActionResult<WeightEntryDto>> AddWeightEntry([FromBody] WeightEntryDto weightEntryDto)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var newWeightEntry = _mapper.Map<WeightEntry>(weightEntryDto);
            newWeightEntry.UserId = userId;

            await _weightEntryRepository.AddWeightEntryAsync(newWeightEntry);
            if (await _weightEntryRepository.SaveAllAsync())
                return CreatedAtRoute("GetWeightEntryById", new { id = newWeightEntry.Id }, weightEntryDto);

            return StatusCode(500, "Could not add new weight entry to database.");
        }

        [HttpDelete("{id:int}")] // api/weightentries/id
        public async Task<ActionResult> DeleteWeightEntry(int id)
        {
            var weightEntry = await _weightEntryRepository.GetWeightEntryByIdAsync(id);
            if (weightEntry == null) return BadRequest("No weight entry found with that id");

            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (weightEntry.UserId != userId) return Forbid("Can not delete other user's entry.");


            _weightEntryRepository.RemoveWeightEntry(weightEntry);

            if (await _weightEntryRepository.SaveAllAsync()) return NoContent();
            return StatusCode(500, "Could not delete weight entry");
        }
    }
}
