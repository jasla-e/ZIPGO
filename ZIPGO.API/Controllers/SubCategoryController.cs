using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("by-main-category/{mainCategoryId}")]
        public async Task<IActionResult> GetByMainCategoryId(int mainCategoryId)
        {
            var subCategories =
                await _subCategoryService.GetByMainCategoryId(mainCategoryId);

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
    }
}