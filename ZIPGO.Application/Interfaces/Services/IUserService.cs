using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();

        Task<User?> GetById(int id);

        Task Add(User user);

        Task Update(User user);

        Task Delete(int id);

    }
}
