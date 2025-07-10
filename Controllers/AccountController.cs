using AutoMapper;
using FitnessTrackerAPI.Entities;
using FitnessTrackerAPI.Interfaces;
using FitnessTrackerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<User> userManager, IMapper mapper,
        ITokenService tokenService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var existingUser = await userManager.Users
                .AnyAsync(u => u.NormalizedUserName == registerDto.UserName.ToUpper()
                || u.NormalizedEmail == registerDto.Email.ToUpper());

            if (existingUser) return BadRequest("User with that email or username already exists.");

            var user = mapper.Map<User>(registerDto);

            user.UserName = registerDto.UserName.ToLower();

            var result = await userManager.CreateAsync(user, registerDto.Password);

            // Add Role
            await userManager.AddToRoleAsync(user, "Member");

            if (!result.Succeeded) return BadRequest("Could not register user");

            var userDto = new UserDto
            {
                FullName = registerDto.FullName,
                Token = await tokenService.CreateToken(user),
                UserName = registerDto.UserName.ToLower(),
            };

            return CreatedAtRoute("GetMemberByIdAsync", new {id = user.Id}, userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var existingUser = await userManager.Users
                .SingleOrDefaultAsync(u => u.NormalizedUserName == loginDto.EmailOrUsername.ToUpper()
                    || u.NormalizedEmail == loginDto.EmailOrUsername.ToUpper());

            if (existingUser == null || existingUser.UserName == null) return NotFound("No user found with that username or email");

            var result = await userManager.CheckPasswordAsync(existingUser, loginDto.Password);

            if (!result) return Unauthorized("Wrong password");

            var userDto = new UserDto
            {
                FullName = existingUser.FullName,
                Token = await tokenService.CreateToken(existingUser),
                UserName = existingUser.UserName,
            };

            return Ok(userDto);
        }

    }
}
