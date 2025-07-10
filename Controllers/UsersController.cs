using FitnessTrackerAPI.Interfaces;
using FitnessTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserRepository userRepository) : ControllerBase
    {
        [HttpGet] // api/users
        public async Task<ActionResult<List<MemberDto>>> GetMembersAsync()
        {
            var users = await userRepository.GetAllMembersAsync();

            return Ok(users);
        }

        [HttpGet("{id:int}", Name = "GetMemberByIdAsync") ] // api/users/id
        public async Task<ActionResult<MemberDto>> GetMemberByIdAsync(int id)
        {
            var user = await userRepository.GetMemberByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{username}")] // api/users/username
        public async Task<ActionResult<MemberDto>> GetMemberByUsername(string username)
        {
            var user = await userRepository.GetMemberByUsernameAsync(username);

            if(user == null) return NotFound();

            return Ok(user);
        }
        
    }
}
