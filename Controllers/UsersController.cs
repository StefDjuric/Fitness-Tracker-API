using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Interfaces;
using FitnessTrackerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitnessTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserRepository userRepository, UserManager<User> userManager) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly UserManager<User> _userManager = userManager;

        [HttpGet] // api/users
        public async Task<ActionResult<List<MemberDto>>> GetMembersAsync()
        {
            var users = await _userRepository.GetAllMembersAsync();

            return Ok(users);
        }

        [HttpGet("{id:int}", Name = "GetMemberByIdAsync") ] // api/users/id
        public async Task<ActionResult<MemberDto>> GetMemberByIdAsync(int id)
        {
            var user = await _userRepository.GetMemberByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{username}")] // api/users/username
        public async Task<ActionResult<MemberDto>> GetMemberByUsername(string username)
        {
            var user = await _userRepository.GetMemberByUsernameAsync(username);

            if(user == null) return NotFound();

            return Ok(user);
        }

        [Authorize]
        [HttpPatch("update")] // api/users/update
        public async Task<ActionResult> PatchUserData([FromBody]UpdateMemberDto updateMemberDto)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var existingUser = await _userRepository.GetUserByIdAsync(userId);

            if (existingUser == null) return NotFound("No user found with that id.");

            if(updateMemberDto.UserName != null) existingUser.UserName = updateMemberDto.UserName;
            if(updateMemberDto.Email != null) existingUser.Email = updateMemberDto.Email;
            if (updateMemberDto.FullName != null) existingUser.FullName = updateMemberDto.FullName;
            if (updateMemberDto.UpdatedPassword != null && updateMemberDto.CurrentPassword != null)
            {
               
                var result = await _userManager.ChangePasswordAsync(existingUser, updateMemberDto.CurrentPassword, updateMemberDto.UpdatedPassword);
                if (!result.Succeeded)
                { 
                     return BadRequest("Could not update password.");
                 }
                return NoContent();
                
            }

            _userRepository.Update(existingUser);

            if(await _userRepository.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Could not update user.");
        }
    }
}
