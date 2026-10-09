using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ZIPGO.Application.DTOs.Auth;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<RegisterDto> _registerValidator;

        private readonly IValidator<LoginDto> _loginValidator;

        public AuthController(
            IAuthService authService,
            IValidator<RegisterDto> registerValidator,
            IValidator<LoginDto> loginValidator)
        {
            _authService = authService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var validationResult = await _registerValidator.ValidateAsync(registerDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => new
                {
                    Field = error.PropertyName,
                    Message = error.ErrorMessage
                }));
            }

            await _authService.Register(registerDto);

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => new
                {
                    Field = error.PropertyName,
                    Message = error.ErrorMessage
                }));
            }

            var result = await _authService.Login(loginDto);

            if (result == null)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(result);
        }
    }
}