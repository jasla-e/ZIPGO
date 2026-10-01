using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Infrastructure.Data
{
    public static class AdminSeeder
    {

        public static async Task SeedAdminAsync(AppDbContext context)
        {
            var AdminExists = await context.Users
                .AnyAsync(u => u.Role == "Admin");



            if (AdminExists)
            {
                return;
            }

            var admin = new User
            {
                Name = "Admin",
                Email = "admin@gmail.com",
                Role = "Admin",
                IsBlocked = false,
                CreatedAt = DateTime.Now
            };

            var passwordHasher = new PasswordHasher<User>();

            admin.PasswordHash = passwordHasher.HashPassword(
                admin,
                "Admin1234"
            );

            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }

    }
}
