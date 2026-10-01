using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {

        Task<User?> GetById(int id);

        Task<User?> GetByEmail(string email);

        Task Add(User user);

        Task Update(User user);

        Task <List<User>> GetAll();

        Task<List<User>> SearchUsers(string search);
    }
}



