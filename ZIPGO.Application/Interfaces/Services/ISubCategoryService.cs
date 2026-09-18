using System;
using System.Collections.Generic;
using System.Text;

using ZIPGO.Application.DTOs.SubCategory;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface ISubCategoryService
    {
        Task<List<SubCategoryDto>> GetAll();

        Task<SubCategoryDto?> GetById(int id);

        Task Add(SubCategoryCreateDto subCategory);

        Task Update(int id, SubCategoryCreateDto subCategory);

        Task Delete(int id);
    }
}