using ZIPGO.Application.DTOs.SubCategory;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.Application.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryService(
            ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<List<SubCategoryDto>> GetAll()
        {
            var subCategories =
                await _subCategoryRepository.GetAll();

            return subCategories.Select(s => new SubCategoryDto
            {
                Id = s.Id,
                Name = s.Name,
                MainCategoryIds = s.MainCategories
                    .Select(m => m.Id)
                    .ToList()
            }).ToList();
        }

        public async Task<List<SubCategoryDto>> GetByMainCategoryId(
            int mainCategoryId)
        {
            var subCategories =
                await _subCategoryRepository
                    .GetByMainCategoryId(mainCategoryId);

            return subCategories.Select(s => new SubCategoryDto
            {
                Id = s.Id,
                Name = s.Name,
                MainCategoryIds = s.MainCategories
                    .Select(m => m.Id)
                    .ToList()
            }).ToList();
        }

        public async Task<SubCategoryDto?> GetById(int id)
        {
            var subCategory =
                await _subCategoryRepository.GetById(id);

            if (subCategory == null)
                return null;

            return new SubCategoryDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                MainCategoryIds = subCategory.MainCategories
                    .Select(m => m.Id)
                    .ToList()
            };
        }
    }
}