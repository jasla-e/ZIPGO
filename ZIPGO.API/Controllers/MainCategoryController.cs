using Microsoft.AspNetCore.Mvc;
using ZIPGO.Application.DTOs.MainCategory;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainCategoryController : ControllerBase
    {
        private readonly IMainCategoryService _mainCategoryService;

        public MainCategoryController(IMainCategoryService mainCategoryService)
        {
            _mainCategoryService = mainCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mainCategories = await _mainCategoryService.GetAll();
            return Ok(mainCategories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mainCategory = await _mainCategoryService.GetById(id);

            if (mainCategory == null)
                return NotFound();

            return Ok(mainCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MainCategoryCreateDto mainCategory)
        {
            await _mainCategoryService.Add(mainCategory);
            return Ok(mainCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            MainCategoryCreateDto mainCategory)
        {
            await _mainCategoryService.Update(id, mainCategory);
            return Ok(mainCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mainCategoryService.Delete(id);
            return Ok("MainCategory deleted successfully");
        }
    }
}