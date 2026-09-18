using ZIPGO.Application.DTOs.SubCategory;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<List<SubCategoryDto>> GetAll()
        {
            var subCategories = await _subCategoryRepository.GetAll();

            return subCategories.Select(s => new SubCategoryDto
            {
                Id = s.Id,
                Name = s.Name,
                MainCategoryId = s.MainCategoryId
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
                MainCategoryId = subCategory.MainCategoryId
            };
        }

        public async Task Add(SubCategoryCreateDto subCategoryDto)
        {
            var subCategory = new SubCategory
            {
                Name = subCategoryDto.Name,
                MainCategoryId = subCategoryDto.MainCategoryId
            };

            await _subCategoryRepository.Add(subCategory);
        }

        public async Task Update(int id, SubCategoryCreateDto subCategoryDto)
        {
            var subCategory = await _subCategoryRepository.GetById(id);

            if (subCategory == null)
                return;

            subCategory.Name = subCategoryDto.Name;
            subCategory.MainCategoryId = subCategoryDto.MainCategoryId;

            await _subCategoryRepository.Update(subCategory);
        }

        public async Task Delete(int id)
        {
            await _subCategoryRepository.Delete(id);
        }
    }
}