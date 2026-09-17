using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Repositories
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetByUserId(int userId);

        Task<Address?>GetById(int id);

        Task Add(Address address);

        Task Update(Address address);

        Task Delete(int id);




    }
}
