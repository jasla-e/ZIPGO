using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;



namespace ZIPGO.Application.Interfaces.Repositories
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategory>> GetAll();
        Task<List<SubCategory>> GetByMainCategoryId(int mainCategoryId);
        Task<SubCategory?> GetById(int id);
    }
}