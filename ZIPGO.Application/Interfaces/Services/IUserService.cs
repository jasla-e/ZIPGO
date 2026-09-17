using ZIPGO.Application.DTOs;
using ZIPGO.Application.DTOs.Auth;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> GetById(int id);
        Task UpdateProfile(int userId, UpdateProfileDto updateProfileDto);
    }
}