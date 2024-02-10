

using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Services;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipDB.Tests.Services;

public class ProductServiceTests
{
    private readonly DataContext _context =
        new(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);
    [Fact]
    public void CreateProduct_Should_CreateProduct_When_ProductDoesNotExist_ReturnNewProduct()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var priceService = new PriceService(new PriceRepository(_context));
        var productService = new ProductService(productRepository, categoryService, priceService);

        // Act
        var result = productService.CreateProduct("Test Make", "Test Model", 2022, "Test Category", 10000);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Make", result.Make);
        Assert.Equal("Test Model", result.Model);
        Assert.Equal(2022, result.Year);
        Assert.Equal("Test Category", result.Category.CategoryName);
        Assert.Equal(10000, result.Price.SellingPrice);
    }

    [Fact]
    public void CreateProduct_ShouldNotCreateProduct_When_ProductAlreadyExists_ReturnNull()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var priceService = new PriceService(new PriceRepository(_context));
        var productService = new ProductService(productRepository, categoryService, priceService);
        productService.CreateProduct("Test Make", "Test Model", 2022, "Test Category", 10000);

        // Act
       
        var result = productService.CreateProduct("Test Make", "Test Model", 2022, "Test Category", 10000);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public void GetProduct_Should_ReturnAllProducts_From_Database_Returns_IEnumerableofTypeProductEntity()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var priceService = new PriceService(new PriceRepository(_context));
        var productService = new ProductService(productRepository, categoryService, priceService);

        // Act

        var result = productService.GetProducts();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProductEntity>>(result);
    }
    [Fact]
    public void GetProductById_Should_RetrieveOneProduct_Return_OneProduct()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var priceService = new PriceService(new PriceRepository(_context));
        var productService = new ProductService(productRepository, categoryService, priceService);

         productService.CreateProduct("Test Make", "Test Model", 2022, "Test Category", 10000);

        // Act
        var result = productService.GetProductById(1);

        // Assert
        Assert.NotNull(result);

    }

    [Fact]
    public void GetProductById_ShouldNot_RetrieveOneProduct_WhenProductDoesnotExists_ReturnNull()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var priceService = new PriceService(new PriceRepository(_context));
        var productService = new ProductService(productRepository, categoryService, priceService);

        // Act
        var result = productService.GetProductById(1);

        // Assert
        Assert.Null(result);

    }

    [Fact]
    public void UpdateProduct_Should_UpdateProduct_Return_UpdatedProduct()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var priceService = new PriceService(new PriceRepository(_context));
        var productService = new ProductService(productRepository, categoryService, priceService);

        var newProduct = productService.CreateProduct("Test Make", "Test Model", 2022, "Test Category", 10000);

        newProduct.Make = "Updated Make";
        newProduct.Model = "Updated Model";
        newProduct.Year = 2023;
        newProduct.Category.CategoryName = "Updated Category";
        newProduct.Price.SellingPrice = decimal.Parse("1001");

        var updatedProduct = productService.UpdateProduct(newProduct);

        // Assert
        Assert.NotNull(updatedProduct);
        Assert.Equal("Updated Make", updatedProduct.Make);
        Assert.Equal("Updated Model", updatedProduct.Model);
        Assert.Equal(2023, updatedProduct.Year);
        Assert.Equal("Updated Category", updatedProduct.Category.CategoryName);
        Assert.Equal(1001,updatedProduct.Price.SellingPrice);

      
    }

    [Fact]
    public void DeleteProduct_Should_FindProductandDeleteIt()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var priceService = new PriceService(new PriceRepository(_context));
        var productService = new ProductService(productRepository, categoryService, priceService); 
        var newProduct = productService.CreateProduct("Test Make", "Test Model", 2022, "Test Category", 10000);


        // Act
       productService.DeleteProduct(newProduct.Id);

        // Assert
        var result = productService.GetProductById(newProduct.Id);
        Assert.Null(result);
    }

    [Fact]
    public void DeleteCustomer_ShouldNotFindCustomerandDeleteIt_ReturnNull()
    {
        // Arrange
        var productRepository = new ProductRepository(_context);
        var categoryService = new CategoryService(new CategoryRepository(_context));
        var priceService = new PriceService(new PriceRepository(_context));
        var productService = new ProductService(productRepository, categoryService, priceService); 
        productService.CreateProduct("Test Make", "Test Model", 2022, "Test Category", 10000);

        // Act
        productService.DeleteProduct(999);

        // Assert
        var result =productService.GetProductById(999);
        Assert.Null(result);
    }



}
