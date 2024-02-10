using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.Tests.Services;

public class PriceServiceTests
{
    private readonly DataContext _context =
        new(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public void CreatePrice_Should_CreateNewSellingPrice_ReturnSellingPrice()
    {
        // Arrange
        var priceRepository = new PriceRepository(_context);
        var priceService = new PriceService(priceRepository);
        decimal sellingPrice = 100;

        // Act
        var result = priceService.CreatePrice(sellingPrice);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sellingPrice, result.SellingPrice);


    }


    [Fact]
    public void GetAllPrices_Should_ReturnAllSellingPrices_From_Database_Returns_IEnumerableofTypePriceEntity()
    {
        // Arrange
        var priceRepository = new PriceRepository(_context);
        var priceService = new PriceService(priceRepository);

        // Act
        var result = priceService.GetPrices();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<PriceEntity>>(result);
    }


    [Fact]
    public void GetPriceById_Should_RetrieveOneSellingPricey_Return_OneSellingPrice()
    {
        // Arrange
        var priceRepository = new PriceRepository(_context);
        var priceService = new PriceService(priceRepository);

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
        var priceRepository = new PriceRepository(_context);
        var priceService = new PriceService(priceRepository);

        // Act
        var result = priceService.GetPriceById(100); 


        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdatePrice_Should_UpdateSellingPrice_Return_UpdatedSellingPrice()
    {
        // Arrange
        var priceRepository = new PriceRepository(_context);
        var priceService = new PriceService(priceRepository);
        var newPrice = priceService.CreatePrice(100);
        newPrice.SellingPrice =101;

        // Act
        var result = priceService.UpdatePrice(newPrice);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(101, result.SellingPrice); 
    }

    [Fact]
    public void DeletePrice_Should_FindSellingPriceandDeleteIt_ReturnTrue()
    {
        // Arrange
        var priceRepository = new PriceRepository(_context);
        var priceService = new PriceService(priceRepository);
        var newPrice = priceService.CreatePrice(100);

        // Act
        priceService.DeletePrice(newPrice.Id);

        // Assert
        var result = priceService.GetPriceById(newPrice.Id);
        Assert.Null(result);
    }

    [Fact]
    public void DeletePrice_ShouldNotFindSellingPriceandDeleteIt()
    {
        // Arrange
        var priceRepository = new PriceRepository(_context);
        var priceService = new PriceService(priceRepository);

        // Act
        priceService.DeletePrice(101); 

        // Assert
        var result = priceService.GetPriceById(101);
        Assert.Null(result);
    }
}









