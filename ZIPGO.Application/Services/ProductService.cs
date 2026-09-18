using ZIPGO.Application.DTOs.Product;
using ZIPGO.Application.Interfaces;
using ZIPGO.Application.Interfaces.Services;
using ZIPGO.Domain.Entities;

namespace ZIPGO.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
                SubCategoryId = product.SubCategoryId
            };
        }

        public async Task Add(ProductCreateDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Rating = productDto.Rating,
                Stock = productDto.Stock,
                Image = productDto.Image,
                Offer = productDto.Offer,
                SubCategoryId = productDto.SubCategoryId
            };

            await _productRepository.Add(product);
        }

        public async Task Update(int id, ProductCreateDto productDto)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return;

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Rating = productDto.Rating;
            product.Stock = productDto.Stock;
            product.Image = productDto.Image;
            product.Offer = productDto.Offer;
            product.SubCategoryId = productDto.SubCategoryId;

            await _productRepository.Update(product);
        }

        public async Task Delete(int id)
        {
            await _productRepository.Delete(id);
        }
    }
}