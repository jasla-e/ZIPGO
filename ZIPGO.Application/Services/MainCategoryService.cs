using ZIPGO.Application.DTOs.MainCategory;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.Application.Services
{
    public class MainCategoryService : IMainCategoryService
    {
        private readonly IMainCategoryRepository _mainCategoryRepository;

        public MainCategoryService(
            IMainCategoryRepository mainCategoryRepository)
        {
            _mainCategoryRepository = mainCategoryRepository;
        }

        public async Task<List<MainCategoryDto>> GetAll()
        {
            var mainCategories = await _mainCategoryRepository.GetAll();

            return mainCategories.Select(m => new MainCategoryDto
            {
                Id = m.Id,
                Name = m.Name
            }).ToList();
        }

        public async Task<MainCategoryDto?> GetById(int id)
        {
            var mainCategory =
                await _mainCategoryRepository.GetById(id);

            if (mainCategory == null)
                return null;

            return new MainCategoryDto
            {
                Id = mainCategory.Id,
                Name = mainCategory.Name
            };
        }
    }
}