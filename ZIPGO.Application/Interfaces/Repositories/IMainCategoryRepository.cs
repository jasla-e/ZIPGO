using System;
using System.Collections.Generic;
using System.Text;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Interfaces.Repositories
{
    public interface IMainCategoryRepository
    {
        Task<List<MainCategory>> GetAll();

        Task<MainCategory?> GetById(int id);

        Task Add(MainCategory mainCategory);

        Task Update(MainCategory mainCategory);

        Task Delete(int id);
    }
}