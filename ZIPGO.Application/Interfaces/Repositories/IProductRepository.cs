using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll();

        Task<Product?> GetById(int id);

        Task Add(Product product);

        Task Update(Product product);

        Task Delete(int id);


    }
}
