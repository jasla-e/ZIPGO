using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.DTOs.Auth;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ZIPGO.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public AuthService(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }

        public Task<string?> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task Register(RegisterDto registerDto)
        {
           var existingUser=await _userRepository.GetByEmail(registerDto.Email);

            if (existingUser != null) {

                throw new Exception("Email already exists");
            }


            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Gender = "Not Specified",
                Phone = "Not Specified",
                Role = "User",
                IsBlocked = false,
                CreatedAt = DateTime.Now
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);
            await _userRepository.Add(user);
        }
    }
}
