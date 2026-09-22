using ZIPGO.Application.DTOs.SubCategory;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMainCategoryRepository _mainCategoryRepository;

        public SubCategoryService(
       ISubCategoryRepository subCategoryRepository,
       IMainCategoryRepository mainCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
            _mainCategoryRepository = mainCategoryRepository;
        }

        public async Task<List<SubCategoryDto>> GetAll()
        {
            var subCategories = await _subCategoryRepository.GetAll();

            return subCategories.Select(s => new SubCategoryDto
            {
                Id = s.Id,
                Name = s.Name,
                MainCategoryIds = s.MainCategories
                    .Select(m => m.Id)
                    .ToList()
            }).ToList();
        }

        public async Task<List<SubCategoryDto>> GetByMainCategoryId(int mainCategoryId)
        {
            var subCategories =
                await _subCategoryRepository.GetByMainCategoryId(mainCategoryId);

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
            var subCategory = await _subCategoryRepository.GetById(id);

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

        public async Task Add(SubCategoryCreateDto subCategoryDto)
        {
            var subCategory = new SubCategory
            {
                Name = subCategoryDto.Name
            };

            foreach (var mainCategoryId in subCategoryDto.MainCategoryIds)
            {
                var mainCategory = await _mainCategoryRepository.GetById(mainCategoryId);

                if (mainCategory == null)
                    throw new Exception($"MainCategory with Id {mainCategoryId} not found");

                subCategory.MainCategories.Add(mainCategory);
            }

            await _subCategoryRepository.Add(subCategory);
        }

        public async Task Update(int id, SubCategoryCreateDto subCategoryDto)
        {
            var subCategory = await _subCategoryRepository.GetById(id);

            if (subCategory == null)
                return;

            subCategory.Name = subCategoryDto.Name;

            subCategory.MainCategories.Clear();

            foreach (var mainCategoryId in subCategoryDto.MainCategoryIds)
            {
                var mainCategory = await _mainCategoryRepository.GetById(mainCategoryId);

                if (mainCategory == null)
                    throw new Exception(
                        $"MainCategory with Id {mainCategoryId} not found");

                subCategory.MainCategories.Add(mainCategory);
            }

            await _subCategoryRepository.Update(subCategory);
        }

        public async Task Delete(int id)
        {
            await _subCategoryRepository.Delete(id);
        }
    }
}