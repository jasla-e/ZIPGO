using ZIPGO.Application.DTOs;
using ZIPGO.Application.DTOs.Admin;
using ZIPGO.Application.DTOs.Auth;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<List<AdminUserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();

            var adminUsers = users.Select(user => new AdminUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Phone = user.Phone,
                Gender = user.Gender,
                IsBlocked = user.IsBlocked
            }).ToList();

            return adminUsers;
        }
        public async Task UpdateProfile(
            int userId,
            UpdateProfileDto updateProfileDto)
        {
            var user = await _userRepository.GetById(userId);

            if (user == null)
                return;

            user.Name = updateProfileDto.Name;
            user.Phone = updateProfileDto.Phone;
            user.Gender = updateProfileDto.Gender;
            user.Dob = updateProfileDto.Dob;

            await _userRepository.Update(user);
        }

        public async Task BlockUser(int Id)
        {
            var user= await _userRepository.GetById(Id);

            if(user == null)
            {
                return;
            }

            user.IsBlocked=!user.IsBlocked;
            await _userRepository.Update(user);
        }

        public async Task<List<AdminUserDto>> SearchUsers(string search)
        {
            var users = await _userRepository.SearchUsers(search);

            return users.Select(user => new AdminUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Role = user.Role,
                IsBlocked = user.IsBlocked
            }).ToList();
        }


    }
}