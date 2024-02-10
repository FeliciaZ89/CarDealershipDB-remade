

using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Services;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipDB.Tests.Services;

public class PricesServiceTests
{
    private readonly AppDBContext _context =
        new(new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public void CreatePrice_Should_CreateNewPrice_ReturnPrice()
    {
        // Arrange
        var priceRepository = new PricesRepository(_context);
        var priceService = new PricesService(priceRepository);
        decimal price = 100;

        // Act
        var result = priceService.CreatePrice(price);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(price, result.Price1);
    }

    [Fact]
    public void GetAllPrices_Should_ReturnAllPrices_From_Database_Returns_IEnumerableofTypePrice()
    {
        // Arrange
        var priceRepository = new PricesRepository(_context);
        var priceService = new PricesService(priceRepository);

        // Act
        var result = priceService.GetPrices();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Price>>(result);
    }

    [Fact]
    public void GetPriceById_Should_RetrieveOnePrice_Return_OnePrice()
    {
        // Arrange
        var priceRepository = new PricesRepository(_context);
        var priceService = new PricesService(priceRepository);

        var newPrice = priceService.CreatePrice(100);

        // Act
        var result = priceService.GetPriceById(newPrice.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newPrice.Id, result.Id);
    }

    [Fact]
    public void GetPriceById_ShouldNotRetrievePrice_When_PriceDoesNotExist_ReturnNull()
    {
        // Arrange
        var priceRepository = new PricesRepository(_context);
        var priceService = new PricesService(priceRepository);

        // Act
        var result = priceService.GetPriceById(100); 

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdatePrice_Should_UpdatePrice_Return_UpdatedPrice()
    {
        // Arrange
        var priceRepository = new PricesRepository(_context);
        var priceService = new PricesService(priceRepository);
        var newPrice = priceService.CreatePrice(100);
        newPrice.Price1 = 101;

        // Act
        var result = priceService.UpdatePrice(newPrice);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(101, result.Price1);
    }

    [Fact]
    public void DeletePrice_Should_FindPriceandDeleteIt_ReturnTrue()
    {
        // Arrange
        var priceRepository = new PricesRepository(_context);
        var priceService = new PricesService(priceRepository);
        var newPrice = priceService.CreatePrice(100);

        // Act
        priceService.DeletePrice(newPrice.Id);

        // Assert
        var result = priceService.GetPriceById(newPrice.Id);
        Assert.Null(result);
    }

    [Fact]
    public void DeletePrice_ShouldNotFindPriceandDeleteIt()
    {
        // Arrange
        var priceRepository = new PricesRepository(_context);
        var priceService = new PricesService(priceRepository);

        // Act
        priceService.DeletePrice(101); 

        // Assert
        var result = priceService.GetPriceById(101);
        Assert.Null(result);
    }

}
