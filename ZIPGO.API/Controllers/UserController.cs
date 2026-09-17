
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZIPGO.Application.DTOs;
using ZIPGO.Application.DTOs.Auth;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )?.Value;

            if (userId == null)
                return Unauthorized();

            var user = await _userService.GetById(int.Parse(userId));

            if (user == null)
                return NotFound();

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Dob = user.Dob
            };

            return Ok(userDto);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateMyProfile(UpdateProfileDto updateProfileDto)
        {
            var userId = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )?.Value;

            if (userId == null)
                return Unauthorized();

            await _userService.UpdateProfile(
                int.Parse(userId),
                updateProfileDto
            );

            return Ok("Profile updated successfully");
        }
    }
}