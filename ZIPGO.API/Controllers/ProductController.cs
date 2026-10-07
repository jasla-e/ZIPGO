using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZIPGO.Application.DTOs.Product;
using ZIPGO.Application.Interfaces.Services;

namespace ZIPGO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered(
    [FromQuery] ProductFilterDto filter)
        {
            var products = await _productService.GetFiltered(filter);

            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(
     [FromForm] ProductCreateDto product,
     IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Image is required.");
            }

            await using var stream = image.OpenReadStream();

            await _productService.Add(
                product,
                stream,
                image.FileName
            );

            return Ok(product);
        }
        
        
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
    int id,
    [FromForm] ProductCreateDto product,
    IFormFile? image)
        {
            Stream? imageStream = null;

            if (image != null && image.Length > 0)
            {
                imageStream = image.OpenReadStream();
            }

            await _productService.Update(
                id,
                product,
                imageStream,
                image?.FileName);

            if (imageStream != null)
            {
                await imageStream.DisposeAsync();
            }

            return Ok(product);
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminProducts(
        string? search = null,
         int page = 1,
         int pageSize = 4)
        {
            var products = await _productService.GetAdminProducts(
                search,
                page,
                pageSize);

            return Ok(products);
        }
    }
}