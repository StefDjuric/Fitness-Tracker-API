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
    public class GoalsController(IGoalsRepository goalsRepository, IMapper mapper) : ControllerBase
    {
        private readonly IGoalsRepository _goalsRepository = goalsRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet] // api/goals
        public async Task<ActionResult<List<GoalsDto>>> GetAllGoals()
        {
            var goals = await _goalsRepository.GetAllGoalsAsync();

            return Ok(goals);
        }

        [HttpGet("{id:int}", Name = "GetGoalsById")] // api/goals/id
        public async Task<ActionResult<GoalsDto?>> GetGoalsById(int id)
        {
            var goal = await _goalsRepository.GetGoalByIdAsync(id);

            if (goal == null) return NotFound("No goal group was found.");

            return Ok(_mapper.Map<GoalsDto>(goal));
        }

        [HttpGet("for-user/{userId:int}")] // api/goals/for-user/id
        public async Task<ActionResult<GoalsDto>> GetGoalsByUserId(int userId)
        {
            var goal = await _goalsRepository.GetGoalsByUserIdAsync(userId);

            if (goal == null) return Ok(new GoalsDto {
                MealsEatenGoal = 0,
                WaterGoalInLiters = 0,
                WeightGoal = 0,
                WorkoutsGoalInWeek = 0 }
            );

            return Ok(_mapper.Map<GoalsDto>(goal));
        }

        [HttpPost("add")] // api/goals/add
        public async Task<ActionResult<GoalsDto>> AddGoals(GoalsDto goalsDto)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            using var transaction = await _goalsRepository.BeginTransactionAsync();

            try
            {
                var userGoals = await _goalsRepository.GetGoalsByUserIdAsync(userId);

                // If there are already goals remove them
                if (userGoals != null)
                {
                    _goalsRepository.RemoveGoals(userGoals);
                }

                // Add Goals
                var goals = _mapper.Map<UserGoals>(goalsDto);
                goals.UserId = userId;
                await _goalsRepository.SetGoalsAsync(goals);

                if (await _goalsRepository.SaveChangesAsync())
                {
                    await transaction.CommitAsync();
                    return CreatedAtRoute("GetGoalsById", new { id = goals.Id }, goalsDto);
                }
                else
                {
                    await transaction.RollbackAsync();
                    return BadRequest("Could not set goals.");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Could not set goals. {ex}");

            }


        }

        [HttpDelete("{id:int}")] // api/goals/id
        public async Task<ActionResult> RemoveGoals(int id) 
        {
            var goals = await _goalsRepository.GetGoalByIdAsync(id);

            if (goals == null) return NotFound("No Goal set found with that id.");
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (userId != goals.UserId) return Forbid("You are not authorized to do this.");

            _goalsRepository.RemoveGoals(goals);

            if(await _goalsRepository.SaveChangesAsync()) return NoContent();

            return BadRequest("Could not remove goal set.");
        }
    }
}
