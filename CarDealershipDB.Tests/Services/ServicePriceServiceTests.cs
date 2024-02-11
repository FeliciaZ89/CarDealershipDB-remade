
using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Services;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipDB.Tests.Services;

public class ServicePriceServiceTests
{
    private readonly ApplicationDBContext _context =
        new(new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public void CreateCost_Should_CreateNewServicePrice_ReturnServicePrice()
    {
        // Arrange
        var servicePriceRepository = new ServicePriceRepository(_context);
        var servicePriceService = new ServicePriceService(servicePriceRepository);


        // Act
        var result = servicePriceService.CreateCost(100);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(100, result.Cost);
    }

    [Fact]
    public void GetAllCosts_Should_ReturnAllServiceCosts_From_Database_Returns_IEnumerableofTypeServicePrice()
    {
        // Arrange
        var servicePriceRepository = new ServicePriceRepository(_context);
        var servicePriceService = new ServicePriceService(servicePriceRepository);

        // Act
      
          var result = servicePriceService.GetCosts();
       
        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ServicePrice>>(result);
     
    }


    [Fact]
    public void GetCostById_Should_RetrieveOneServicePrice_Return_OneServicePrice()
    {
        // Arrange
        var servicePriceRepository = new ServicePriceRepository(_context);
        var servicePriceService = new ServicePriceService(servicePriceRepository);

        var newCost = servicePriceService.CreateCost(100);

        // Act
        var result = servicePriceService.GetCostById(newCost.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newCost.Id, result.Id);
    }

    [Fact]
    public void GetCostById_ShouldNotRetrieveServicePrice_When_PriceDoesNotExist_ReturnNull()
    {
        // Arrange
        var servicePriceRepository = new ServicePriceRepository(_context);
        var servicePriceService = new ServicePriceService(servicePriceRepository);

        // Act
        var result = servicePriceService.GetCostById(100);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateCost_Should_UpdateServicePrice_Return_UpdatedServicePrice()
    {
        // Arrange
        var servicePriceRepository = new ServicePriceRepository(_context);
        var servicePriceService = new ServicePriceService(servicePriceRepository);
        var newCost = servicePriceService.CreateCost(100);
        newCost.Cost = 101;

        // Act
        var result = servicePriceService.UpdateCost(newCost);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(101, result.Cost);
    }

    [Fact]
    public void DeleteCost_Should_FindServicePriceandDeleteIt_ReturnTrue()
    {
        // Arrange
        var servicePriceRepository = new ServicePriceRepository(_context);
        var servicePriceService = new ServicePriceService(servicePriceRepository);
        var newCost = servicePriceService.CreateCost(100);

        // Act
        servicePriceService.DeleteCost(newCost.Id);

        // Assert
        var result = servicePriceService.GetCostById(newCost.Id);
        Assert.Null(result);
    }

    [Fact]
    public void DeleteCost_ShouldNotFindServicePriceandDeleteIt()
    {
        // Arrange
        var servicePriceRepository = new ServicePriceRepository(_context);
        var servicePriceService = new ServicePriceService(servicePriceRepository);

        // Act
        servicePriceService.DeleteCost(101); 

        // Assert
        var result = servicePriceService.GetCostById(101);
        Assert.Null(result);
    }

    
}


