using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Domain.Entities;
using ZIPGO.Infrastructure.Data;

namespace ZIPGO.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task Delete(int id)
        {
            var User=await _context.Users.FindAsync(id);
            if (User != null) { 
            
             _context.Users.Remove(User);
                await _context.SaveChangesAsync();
            
            }
        }

        public async Task<List<User>> GetAll()
        {
           return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Update(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.PasswordHash = user.PasswordHash;
                existingUser.Phone = user.Phone;
                existingUser.Gender = user.Gender;
                existingUser.Dob = user.Dob;
                existingUser.Role = user.Role;
                existingUser.IsBlocked = user.IsBlocked;
                existingUser.CreatedAt = user.CreatedAt;

                await _context.SaveChangesAsync();
            }
        }
    }
}
