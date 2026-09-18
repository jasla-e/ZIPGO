using Microsoft.AspNetCore.Mvc;
using ZIPGO.Application.DTOs.SubCategory;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subCategories = await _subCategoryService.GetAll();
            return Ok(subCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var subCategory = await _subCategoryService.GetById(id);

            if (subCategory == null)
                return NotFound();

            return Ok(subCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubCategoryCreateDto subCategory)
        {
            await _subCategoryService.Add(subCategory);

            return Ok(subCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            SubCategoryCreateDto subCategory)
        {
            await _subCategoryService.Update(id, subCategory);

            return Ok(subCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _subCategoryService.Delete(id);

            return Ok("SubCategory deleted successfully");
        }
    }
}