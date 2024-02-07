using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private DataContext _context;
        private ProductRepository _repo;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            _context = new DataContext(options);
            _repo = new ProductRepository(_context);
        }
        [Fact]
        public void Create_Should_Create_ProductEntity_To_Database_Then_Return_ProductEntity_WithId1()
        {
            // Arrange
            var productEntity = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } };

            // Act
            var result = _repo.Create(productEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Create_Should_Not_SaveRecord_To_Database_Return_Null()
        {
            // Arrange
            var productRepository = new ProductRepository(_context);
            var productEntity = new ProductEntity();

            // Act
            var result = _repo.Create(productEntity);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void Get_Retrieves_AllRecords_From_ProductEntity_Returns_IEnumerableofTypeProductEntity()
        {
            // Arrange

            // Act
            var result = _repo.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ProductEntity>>(result);
        }

        [Fact]
        public void Get_ShouldRetrieve_OneProductById_Return_One_Product()
        {
            // Arrange
            var productRepository = new ProductRepository(_context);
           var productEntity = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } };
            _repo.Create(productEntity);

            // Act
     

            var result = _repo.Get(x => x.Id == productEntity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productEntity.Id, result.Id);
        }

        [Fact]
        public void Get_ShouldNotFind_OneProductById_Return_Null()
        {
            // Arrange
            var productEntity = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } };

            // Act
            var result = _repo.Get(x => x.Id == productEntity.Id);

            // Assert
            Assert.Null(result);
        }
        [Fact] //overridden method
        public void Get_ShouldRetrieve_OneProductWithPriceAndCategory_Return_One_Product()
        {
            // Arrange
            var priceEntity = new PriceEntity { SellingPrice = 100 };
            var categoryEntity = new CategoryEntity { CategoryName = "Test Category" };
            var productEntity = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = priceEntity, Category = categoryEntity };
            _repo.Create(productEntity);

            // Act
            var result = _repo.Get(x => x.Id == productEntity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productEntity.Id, result.Id);
            Assert.NotNull(result.Price);
            Assert.NotNull(result.Category);
        }
        [Fact]
        public void Update_ShouldUpdateExistingProduct_ReturnUpdatedProduct()
        {
            // Arrange
            var productEntity = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } };
            productEntity = _repo.Create(productEntity);

            // Act
            productEntity.Make = "Testing Make";
            productEntity.Model = "Testing Model";
            productEntity.Year = 2023;
            productEntity.Price.SellingPrice = 200;
            productEntity.Category.CategoryName = "Testing Category";
            var result = _repo.Update(x => x.Id == productEntity.Id, productEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productEntity.Id, result.Id);
            Assert.Equal("Testing Make", result.Make);
            Assert.Equal("Testing Model", result.Model);
            Assert.Equal(2023, result.Year);
            Assert.Equal(200, result.Price.SellingPrice);
            Assert.Equal("Testing Category", result.Category.CategoryName);
        }

        [Fact]
        public void Update_ShouldNotUpdateNonexistentProduct_ReturnNull()
        {
            // Arrange
            var nonExistentProduct = new ProductEntity { Id = 999, Make = "Nonexistent Make", Model = "Nonexistent Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Nonexistent Category" } };

            // Act
            var result = _repo.Update(x => x.Id == nonExistentProduct.Id, nonExistentProduct);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Delete_ShouldRemoveOneProduct_Return_True()
        {
            // Arrange
            var productEntity = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } };
            _repo.Create(productEntity);

            // Act
            var result = _repo.Delete(x => x.Id == productEntity.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_ShouldNotFindProductAndRemoveIt_Return_False()
        {
            // Arrange
            var productEntity = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } };

            // Act
            var result = _repo.Delete(x => x.Id == productEntity.Id);

            // Assert
            Assert.False(result);
        }


    }





}
