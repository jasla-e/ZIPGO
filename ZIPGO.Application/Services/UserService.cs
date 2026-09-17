using ZIPGO.Application.DTOs;
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
    }
}