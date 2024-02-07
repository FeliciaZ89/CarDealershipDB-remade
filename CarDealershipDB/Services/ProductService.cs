using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarDealershipDB.Services
{
    public class ProductService(ProductRepository productRepository, CategoryService categoryService, PriceService priceService)
    {
        private readonly ProductRepository _productRepository = productRepository;
        private readonly CategoryService _categoryService = categoryService;
        private readonly PriceService _priceService = priceService;

        public ProductEntity CreateProduct(string make, string model, int year, string categoryName, decimal sellingPrice)
        {
            try
            {
                var categoryEntity = _categoryService.CreateCategory(categoryName);
                var priceEntity = _priceService.CreatePrice(sellingPrice);
                var productEntity = new ProductEntity
                {
                    Make = make,
                    Model = model,
                    Year = year,
                    CategoryId = categoryEntity.Id,
                    PriceId = priceEntity.Id,
                };

                productEntity = _productRepository.Create(productEntity);
                return productEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public ProductEntity GetProductById(int id)
        {
            try
            {
                var productEntity = _productRepository.Get(x => x.Id == id);
                return productEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;

        }
        public IEnumerable<ProductEntity> GetProducts()
        {
            try
            {
                var products = _productRepository.GetAll()
                                                 .Include(p => p.Category)
                                                 .Include(p => p.Price)
                                                 .ToList();
                return products;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }


        public ProductEntity UpdateProduct(ProductEntity productEntity)
        {
            try
            {
                var updatedProductEntity = _productRepository.Update(x => x.Id == productEntity.Id, productEntity);
                return updatedProductEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var product = _productRepository.Get(x => x.Id == id);
                if (product != null)
                {
                    var priceId = product.PriceId;
                    _productRepository.Delete(x => x.Id == id);
                    _priceService.DeletePrice(priceId);
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
        }

    }
}
