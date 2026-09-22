using System;
using System.Collections.Generic;
using System.Text;

using ZIPGO.Application.DTOs.SubCategory;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Services
{
    public interface ISubCategoryService
    {
        Task<List<SubCategoryDto>> GetAll();

        Task<List<SubCategoryDto>> GetByMainCategoryId(int mainCategoryId);

        Task<SubCategoryDto?> GetById(int id);

        Task Add(SubCategoryCreateDto subCategory);

        Task Update(int id, SubCategoryCreateDto subCategory);

        Task Delete(int id);
    }
}