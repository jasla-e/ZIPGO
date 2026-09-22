using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;
using ZIPGO.Application.DTOs.Product;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAll();

        Task<ProductDto?> GetById(int id);

        Task<List<ProductDto>> GetFiltered(ProductFilterDto filter);
        Task Add(ProductCreateDto product);

        Task Update(int id, ProductCreateDto product);

        Task Delete(int id);
    }
}