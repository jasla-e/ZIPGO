using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;
using ZIPGO.Application.DTOs.MainCategory;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface IMainCategoryService
    {
        Task<List<MainCategoryDto>> GetAll();

        Task<MainCategoryDto?> GetById(int id);

        Task Add(MainCategoryCreateDto mainCategory);

        Task Update(int id, MainCategoryCreateDto mainCategory);

        Task Delete(int id);
    }
}