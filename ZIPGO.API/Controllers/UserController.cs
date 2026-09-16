
using Microsoft.AspNetCore.Mvc;
using ZIPGO.Application.DTOs;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Dob = user.Dob
            }).ToList();

            return Ok(userDtos);
        }

        // GET: api/User/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);

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

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            await _userService.Add(user);

            return Ok(user);
        }

        // PUT: api/User/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            user.Id = id;

            await _userService.Update(user);

            return Ok(user);
        }

        // DELETE: api/User/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);

            return Ok();
        }
    }
}


