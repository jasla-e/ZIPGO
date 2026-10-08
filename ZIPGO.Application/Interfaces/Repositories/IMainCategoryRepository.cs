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
    }
}