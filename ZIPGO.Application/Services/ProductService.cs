using ZIPGO.Application.DTOs.Product;
using ZIPGO.Application.Interfaces;
using ZIPGO.Application.Interfaces.Repositories;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICloudinaryService _cloudinaryService;

        public ProductService(
        IProductRepository productRepository,
        ISubCategoryRepository subCategoryRepository,
        ICloudinaryService cloudinaryService)
        {
            _productRepository = productRepository;
            _subCategoryRepository = subCategoryRepository;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _productRepository.GetAll();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Rating = p.Rating,
                Stock = p.Stock,
                Image = p.Image,
                Offer = p.Offer,
                MainCategoryId = p.MainCategoryId,
                SubCategoryId = p.SubCategoryId
            }).ToList();
        }

        public async Task<ProductDto?> GetById(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Rating = product.Rating,
                Stock = product.Stock,
                Image = product.Image,
                Offer = product.Offer,
                MainCategoryId = product.MainCategoryId,
                SubCategoryId = product.SubCategoryId
            };
        }

        public async Task<List<ProductDto>> GetFiltered(ProductFilterDto filter)
        {
            var products = await _productRepository.GetFiltered(filter);

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Rating = p.Rating,
                Stock = p.Stock,
                Image = p.Image,
                Offer = p.Offer,
                MainCategoryId = p.MainCategoryId,
                SubCategoryId = p.SubCategoryId
            }).ToList();
        }

        public async Task Add(ProductCreateDto productDto,Stream imageStream, string fileName)
        {
           
            var subCategory = await _subCategoryRepository.GetById(
                productDto.SubCategoryId);

            if (subCategory == null)
                throw new Exception("SubCategory not found");

       var belongsToMainCategory =
       await _subCategoryRepository.BelongsToMainCategory(
        productDto.SubCategoryId,
        productDto.MainCategoryId);

            if (!belongsToMainCategory)
                throw new Exception("SubCategory does not belong to the selected MainCategory");
        
         var imageUrl = await _cloudinaryService.UploadImage(
         imageStream,
         fileName);
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Rating = productDto.Rating,
                Stock = productDto.Stock,
                Image = imageUrl,
                Offer = productDto.Offer,
                MainCategoryId = productDto.MainCategoryId,
                SubCategoryId = productDto.SubCategoryId
            };

            await _productRepository.Add(product);
        }

        public async Task Update( int id,ProductCreateDto productDto,Stream? imageStream,string? fileName)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return;

            var subCategory = await _subCategoryRepository.GetById(
                productDto.SubCategoryId);

            if (subCategory == null)
                throw new Exception("SubCategory not found");

            var belongsToMainCategory =
         await _subCategoryRepository.BelongsToMainCategory(
        productDto.SubCategoryId,
        productDto.MainCategoryId);

            if (!belongsToMainCategory)
                throw new Exception("SubCategory does not belong to the selected MainCategory");

            if (imageStream != null && !string.IsNullOrEmpty(fileName))
            {
                var imageUrl = await _cloudinaryService.UploadImage(
                    imageStream,
                    fileName);

                product.Image = imageUrl;
            }

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Rating = productDto.Rating;
            product.Stock = productDto.Stock;
            product.Offer = productDto.Offer;
            product.MainCategoryId = productDto.MainCategoryId;
            product.SubCategoryId = productDto.SubCategoryId;

            await _productRepository.Update(product);
        }

        public async Task Delete(int id)
        {
            await _productRepository.Delete(id);
        }

        public async Task<ProductPagedResultDto> GetAdminProducts(
    string? search,
    int page,
    int pageSize)
        {
            var result = await _productRepository.GetAdminProducts(
                search,
                page,
                pageSize);

            var products = result.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Rating = p.Rating,
                Stock = p.Stock,
                Image = p.Image,
                Offer = p.Offer,
                MainCategoryId = p.MainCategoryId,
                SubCategoryId = p.SubCategoryId
            }).ToList();

            return new ProductPagedResultDto
            {
                Products = products,
                TotalCount = result.TotalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(
                    result.TotalCount / (double)pageSize)
            };
        }
    }
}