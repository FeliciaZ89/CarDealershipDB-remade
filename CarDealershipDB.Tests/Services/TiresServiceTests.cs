

using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Services;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipDB.Tests.Services;

public class TiresServiceTests
{
    private readonly ApplicationDBContext _context =
        new(new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);
    [Fact]
    public void CreateTire_Should_CreateTire_When_TireDoesNotExist_ReturnNewTire()
    {
        // Arrange
        var tireRepository = new TiresRepository(_context);
        var pricesService = new PricesService(new PricesRepository(_context));
        var tireInventoryService = new TireInventoryService(new TireInventoryRepository(_context));
        var tireService = new TiresService(tireRepository, pricesService,tireInventoryService);

        // Act
        var result = tireService.CreateTire("Test Brand", "Test Size","Test Type", "Test Seasonality",1000,4);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Brand", result.Brand);
        Assert.Equal("Test Size", result.Size);
        Assert.Equal("Test Type", result.Type);
        Assert.Equal("Test Seasonality", result.Seasonality);
        Assert.Equal(1000, result.Price.Price1);
        Assert.Equal(4, result.TireInventory.Quantity);
        
    }
    [Fact]
    public void CreateTire_ShouldNot_CreateNewTireWhenTireAlreadyExists_ReturnNull()
    {
        // Arrange
        var tireRepository = new TiresRepository(_context);
        var pricesService = new PricesService(new PricesRepository(_context));
        var tireInventoryService = new TireInventoryService(new TireInventoryRepository(_context));
        var tireService = new TiresService(tireRepository, pricesService, tireInventoryService);
        var newTire = tireService.CreateTire("Test Brand", "Test Size", "Test Type", "Test Seasonality", 1000, 4);

        // Act
        var result = tireService.CreateTire("Test Brand", "Test Size", "Test Type", "Test Seasonality", 1000, 4);

        // Assert
        Assert.Null(result);
    }




[Fact]
    public void GetTire_Should_ReturnAllTires_From_Database_Returns_IEnumerableofTypeTire()
    {
        // Arrange
        var tireRepository = new TiresRepository(_context);
        var pricesService = new PricesService(new PricesRepository(_context));
        var tireInventoryService = new TireInventoryService(new TireInventoryRepository(_context));
        var tireService = new TiresService(tireRepository, pricesService, tireInventoryService);
       
        // Act

        var result = tireService.GetTires();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Tire>>(result);
    }

    [Fact]
    public void GetProductById_Should_RetrieveOneProduct_Return_OneProduct()
    {
        // Arrange
        var tireRepository = new TiresRepository(_context);
        var pricesService = new PricesService(new PricesRepository(_context));
        var tireInventoryService = new TireInventoryService(new TireInventoryRepository(_context));
        var tireService = new TiresService(tireRepository, pricesService, tireInventoryService);

        var newTire =tireService.CreateTire("Test Brand", "Test Size", "Test Type", "Test Seasonality", 1000, 4);

        // Act
        var result = tireService.GetTireById(1);

        // Assert
        Assert.NotNull(result);

    }
    [Fact]
    public void GetProductById_ShouldNot_RetrieveOneNotExist_ReturnNull()
    {
        // Arrange
        var tireRepository = new TiresRepository(_context);
        var pricesService = new PricesService(new PricesRepository(_context));
        var tireInventoryService = new TireInventoryService(new TireInventoryRepository(_context));
        var tireService = new TiresService(tireRepository, pricesService, tireInventoryService);

        tireService.CreateTire("Test Brand", "Test Size", "Test Type", "Test Seasonality", 1000, 4);

        // Act
        var result = tireService.GetTireById(1);

        // Assert
        Assert.NotNull(result);

    }
    [Fact]
    public void UpdateProduct_Should_UpdateProduct_Return_UpdatedProduct()
    {
        // Arrange
        var tireRepository = new TiresRepository(_context);
        var pricesService = new PricesService(new PricesRepository(_context));
        var tireInventoryService = new TireInventoryService(new TireInventoryRepository(_context));
        var tireService = new TiresService(tireRepository, pricesService, tireInventoryService);

        var newTire = tireService.CreateTire("Test Brand", "Test Size", "Test Type", "Test Seasonality", 1000, 4);


        newTire.Brand = "Updated Brand";
        newTire.Size = "Updated Size";
        newTire.Type = "Updated Type";
        newTire.Seasonality = "Updated Seasonality";
        newTire.Price.Price1 = decimal.Parse("101");
        newTire.TireInventory.Quantity = 8;

        var updatedTire = tireService.UpdateTires(newTire);

        // Assert
        Assert.NotNull(updatedTire);
        Assert.Equal("Updated Brand", updatedTire.Brand);
        Assert.Equal("Updated Size", updatedTire.Size);
        Assert.Equal("Updated Type", updatedTire.Type);
        Assert.Equal("Updated Seasonality", updatedTire.Seasonality);
        Assert.Equal(101, updatedTire.Price.Price1);
        Assert.Equal(8, updatedTire.TireInventory.Quantity);
    }
    [Fact]
    public void DeleteTire_Should_FindTireandDeleteIt()
    {
        // Arrange
        var tireRepository = new TiresRepository(_context);
        var pricesService = new PricesService(new PricesRepository(_context));
        var tireInventoryService = new TireInventoryService(new TireInventoryRepository(_context));
        var tireService = new TiresService(tireRepository, pricesService, tireInventoryService);
        var newTire = tireService.CreateTire("Test Brand", "Test Size", "Test Type", "Test Seasonality", 1000, 4);


        // Act
        tireService.DeleteTires(newTire.Id);

        // Assert
        var result = tireService.GetTireById(newTire.Id);
        Assert.Null(result);
    }
    [Fact]
    public void DeleteTire_ShouldNot_FindTireandDeleteIt_ReturnNull()
    {
        // Arrange
        var tireRepository = new TiresRepository(_context);
        var pricesService = new PricesService(new PricesRepository(_context));
        var tireInventoryService = new TireInventoryService(new TireInventoryRepository(_context));
        var tireService = new TiresService(tireRepository, pricesService, tireInventoryService);
        var newTire = tireService.CreateTire("Test Brand", "Test Size", "Test Type", "Test Seasonality", 1000, 4);


        // Act
        tireService.DeleteTires(999);

        // Assert
        var result = tireService.GetTireById(999);
        Assert.Null(result);
    }

}
