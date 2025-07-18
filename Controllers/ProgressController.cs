using AutoMapper;
using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Interfaces;
using FitnessTrackerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTrackerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController(IWeeklyProgressRepository weeklyProgressRepository, IMapper mapper) : ControllerBase
    {
        private readonly IWeeklyProgressRepository _weeklyProgressRepository = weeklyProgressRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet] // api/progress
        public async Task<ActionResult<List<WeeklyProgressDto>>> GetAllWeeklyProgresses()
        {
            var progresses = await _weeklyProgressRepository.GetAllWeeklyProgressAsync();

            return Ok(progresses);
        }

        [HttpGet("{id:int}", Name = "GetWeeklyProgressById")] // api/progress/id
        public async Task<ActionResult<WeeklyProgressDto?>> GetWeeklyProgressById(int id)
        {
            var weeklyProgress = await _weeklyProgressRepository.GetWeeklyProgressByIdAsync(id);

            if (weeklyProgress == null) return NotFound("Weekly Progress not found.");

            return Ok(_mapper.Map<WeeklyProgressDto>(weeklyProgress));
        }

        [HttpGet("for-user/{userId:int}")] // api/progress/for-user/userid
        public async Task<ActionResult<WeeklyProgressDto?>> GetWeeklyProgressByUserId(int userId)
        {
            var weeklyProgress = await _weeklyProgressRepository.GetWeeklyProgressByUserIdAsync(userId);

            if (weeklyProgress == null) return Ok(new WeeklyProgressDto
            {
                MealsEaten = 0,
                WaterConsumed = 0,
                WeekStartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                WorkoutsDone = 0
            });

            return Ok(_mapper.Map<WeeklyProgressDto>(weeklyProgress));
        }

        [HttpPost("add")] // api/progress/add
        public async Task<ActionResult> CreateWeeklyProgress()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            using var transaction = await _weeklyProgressRepository.BeginTransactionAsync();

            try
            {
                var weeklyProgress = await _weeklyProgressRepository.GetWeeklyProgressByUserIdAsync(userId);

                if (weeklyProgress != null)
                {
                    _weeklyProgressRepository.RemoveWeeklyProgress(weeklyProgress);
                }

                var newWeeklyProgress = new WeeklyProgress
                {
                    UserId = userId,
                    MealsEaten = 0,
                    WaterConsumed = 0,
                    WorkoutsDone = 0,
                    WeekStartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    UpdatedAt = DateTime.UtcNow,
                };

                await _weeklyProgressRepository.InitializeWeeklyProgressAsync(newWeeklyProgress);

                if (await _weeklyProgressRepository.SaveChangesAsync())
                {
                    await transaction.CommitAsync();
                    return CreatedAtRoute("GetWeeklyProgressById", new { id = newWeeklyProgress.Id }, _mapper.Map<WeeklyProgressDto>(newWeeklyProgress));
                }
                else
                {
                    await transaction.RollbackAsync();
                    return BadRequest("Could not create new weekly progress.");
                }

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Could not create new weekly progress. {ex}");
            }
        }

        [HttpDelete("{id:int}")] // api/progress/id
        public async Task<ActionResult> DeleteWeeklyProgressById(int id)
        {
            var existingWeeklyProgress = await _weeklyProgressRepository.GetWeeklyProgressByIdAsync(id);

            if (existingWeeklyProgress == null) return NotFound("No weekly progress found.");

            _weeklyProgressRepository.RemoveWeeklyProgress(existingWeeklyProgress);

            if(await _weeklyProgressRepository.SaveChangesAsync()) return NoContent();

            return BadRequest("Could not delete weekly progress.");
        }

        [HttpPatch("{userId:int}")] // api/progress/userId
        public async Task<ActionResult> PatchWeeklyProgressByUserId(int userId, [FromBody] WeeklyProgressUpdateDto updateDto)
        {
            var existingWeeklyProgress = await _weeklyProgressRepository.GetWeeklyProgressByUserIdAsync(userId);

            if (existingWeeklyProgress == null) return NotFound("No weekly progress found. Please set your goals.");

            if (updateDto.MealsEaten != null) existingWeeklyProgress.MealsEaten = (int)updateDto.MealsEaten;

            if (updateDto.WorkoutsDone != null) existingWeeklyProgress.WorkoutsDone = (int)updateDto.WorkoutsDone;

            if (updateDto.WaterConsumed != null) existingWeeklyProgress.WaterConsumed = (float)updateDto.WaterConsumed;


            _weeklyProgressRepository.Update(existingWeeklyProgress);

            if (await _weeklyProgressRepository.SaveChangesAsync()) return NoContent();

            return StatusCode(500, "Could not update weekly progress.");
        }
    }
}
