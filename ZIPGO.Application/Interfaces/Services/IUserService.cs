using ZIPGO.Application.DTOs;
using ZIPGO.Application.DTOs.Admin;
using ZIPGO.Application.DTOs.Auth;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> GetById(int id);
        Task<List<AdminUserDto>> GetAllUsers();
        Task UpdateProfile(int userId, UpdateProfileDto updateProfileDto);
        Task BlockUser(int Id);
        Task<List<AdminUserDto>> SearchUsers(string search);
    }
}