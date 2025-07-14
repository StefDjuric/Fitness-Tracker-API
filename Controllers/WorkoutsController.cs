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
    public class WorkoutsController(IWorkoutRepository workoutRepository
        , IUserRepository userRepository, IMapper mapper) : ControllerBase
    {
        private readonly IWorkoutRepository _workoutRepository = workoutRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet] // api/workouts
        public async Task<ActionResult<List<WorkoutDto>>> GetAllWorkouts()
        {
            var workouts = await _workoutRepository.GetAllWorkoutsAsync();

            return Ok(_mapper.Map<List<WorkoutDto>>(workouts));
        }

        [HttpGet("for-user/{userId:int}")] // api/workouts/for-user/id
        public async Task<ActionResult<List<WorkoutDto>>> GetAllWorkoutsForUserById(int userId)
        {
            if (userId < 0) return BadRequest("UserId should be zero or positive");

            var user = await _userRepository.GetMemberByIdAsync(userId);
            if (user == null) return NotFound("No user with that id is found");

            var workouts = await _workoutRepository.GetAllWorkoutsForUserAsync(userId);

            return Ok(_mapper.Map<List<WorkoutDto>>(workouts));

        }

        [HttpGet("get-runs/{userId:int}")] // api/workouts/get-runs/id
        public async Task<ActionResult<List<WorkoutDto>>> GetAllRunsByUserId(int userId)
        {
            if (userId < 0) return BadRequest("UserId should be zero or positive");

            var user = await _userRepository.GetMemberByIdAsync(userId);
            if (user == null) return NotFound("No user with that id is found");

            var runs = await _workoutRepository.GetRunningWorkoutsForUserAsync(userId);
            return Ok(_mapper.Map<List<WorkoutDto>>(runs));
        }

        [HttpGet("get-gym-workouts/{userId:int}")] // api/workouts/get-gym-workouts/id
        public async Task<ActionResult<List<WorkoutDto>>> GetAllWeightliftingsByUserId(int userId)
        {
            if (userId < 0) return BadRequest("UserId should be zero or positive");

            var user = await _userRepository.GetMemberByIdAsync(userId);
            if (user == null) return NotFound("No user with that id is found");

            var weightligtings = await _workoutRepository.GetWeightliftingWorkoutsForUserAsync(userId);
            return Ok(_mapper.Map<List<WorkoutDto>>(weightligtings));
        }

        [HttpGet("{id:int}", Name = "GetWorkoutById")] // api/workouts/id
        public async Task<ActionResult<WorkoutDto>> GetWorkoutById(int id)
        {
            var workout = await _workoutRepository.GetWorkoutByIdAsync(id);
            if (workout == null) return NotFound("No workout has been found.");
            
            return Ok(_mapper.Map<WorkoutDto>(workout));
        }

        [HttpGet("get-by-type/{userId:int}")] // api/workouts/get-by-type/id
        public async Task<ActionResult<List<WorkoutDto>>> GetWorkoutsByType([FromQuery] string type, int userId)
        {
            if (type == null) return BadRequest("No type parameter found in query");

            if (userId < 0) return BadRequest("UserId should be zero or positive");

            var user = await _userRepository.GetMemberByIdAsync(userId);
            if (user == null) return NotFound("No user with that id is found");

            var workouts = await _workoutRepository.GetWorkoutsForUserByTypeAsync(type, userId);
            return Ok(_mapper.Map<List<WorkoutDto>>(workouts));
        }

        [HttpPost("add")] // api/workouts/add
        public async Task<ActionResult<WorkoutDto>> CreateWorkout([FromBody]WorkoutDto workoutDto)
        {
            var workout = _mapper.Map<Workout>(workoutDto);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) return BadRequest("Could not find the user id in token.");
            workout.UserId = Convert.ToInt32(userId);
            
            if(workout.WeightliftingLog != null)
            {
                workout.WeightliftingLog.WorkoutId = workout.Id;
            }
            else if(workout.RunLog != null)
            {
                workout.RunLog.WorkoutId = workout.Id;
            }

            await _workoutRepository.AddWorkoutAsync(workout);

            if (await _workoutRepository.SaveAllAsync()) return CreatedAtRoute("GetWorkoutById", new { id = workout.Id }, workoutDto);
            else return BadRequest("Could not add workout");
        }

        [HttpDelete("delete/{id:int}")] // api/workouts/delete/id
        public async Task<ActionResult> DeleteWorkout(int id)
        {
            if (id < 0) return BadRequest("Id should be zero or positive");

            var workout = await _workoutRepository.GetWorkoutByIdAsync(id);
            if (workout == null) return NotFound("No workout found with that id.");
            _workoutRepository.DeleteWorkout(workout);


            if (await _workoutRepository.SaveAllAsync()) return NoContent();

            else return BadRequest("Could not delete workout.");
        }

        [HttpGet("count/{userId:int}")] // api/workouts/count
        public async Task<ActionResult<int>> GetUserWorkoutCount(int userId)
        {
            int workoutCount = await _workoutRepository.GetUserWorkoutCountAsync(userId);


            return Ok(workoutCount);
        }

        [HttpGet("run-count/{userId:int}")] // api/workouts/run-count
        public async Task<ActionResult<int>> GetUserRunCount(int userId)
        {
            int workoutCount = await _workoutRepository.GetUserRunCountAsync(userId);

            return Ok(workoutCount);
        }

        [HttpGet("lifting-count/{userId:int}")] // api/workouts/lifting-count
        public async Task<ActionResult<int>> GetUserLiftingCount(int userId)
        {
            int workoutCount = await _workoutRepository.GetUserWeightliftingCountAsync(userId);

            return Ok(workoutCount);
        }
    }
}
