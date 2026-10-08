using Microsoft.AspNetCore.Mvc;
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
    }
}