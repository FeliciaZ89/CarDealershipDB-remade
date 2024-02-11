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

namespace CarDealershipDB.Tests.Services;

public class TireServicesServiceTests
{
    private readonly ApplicationDBContext _context =
        new(new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public void CreateTireServices_Should_CreateNewTireService_When_TireServiceNotExist_ReturnNewTireService()
    {
        // Arrange
        var tireServiceRepository = new TireServicesRepository(_context);
        var servicePriceService = new ServicePriceService(new ServicePriceRepository(_context));
        var tireServiceService = new TireServicesService(tireServiceRepository, servicePriceService);

        // Act
        var result = tireServiceService.CreateTireService("Test service", 2500);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test service", result.ServiceName);
        Assert.Equal(2500, result.Cost.Cost);
    }

    [Fact]
    public void CreateTireService_ShouldNot_CreateNewTireServiceWhenTireServiceAlreadyExists_ReturnNull()
    {
        // Arrange
        var tireServiceRepository = new TireServicesRepository(_context);
        var servicePriceService = new ServicePriceService(new ServicePriceRepository(_context));
        var tireServiceService = new TireServicesService(tireServiceRepository, servicePriceService);
        var newTireService = tireServiceService.CreateTireService("Test service", 2500);

        // Act
        var result = tireServiceService.CreateTireService("Test service", 2500);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public void GetTireServices_Should_ReturnAllTireServices_From_Database_Returns_IEnumerableofTypeTireService()
    {
        // Arrange
        var tireServiceRepository = new TireServicesRepository(_context);
        var servicePriceService = new ServicePriceService(new ServicePriceRepository(_context));
        var tireServiceService = new TireServicesService(tireServiceRepository, servicePriceService);

        // Act
        var result = tireServiceService.GetTireServices();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<TireService>>(result);
    }

    [Fact]
    public void GetTireServiceById_Should_RetrieveOneTireService_Return_OneTireService()
    {
        // Arrange
        var tireServiceRepository = new TireServicesRepository(_context);
        var servicePriceService = new ServicePriceService(new ServicePriceRepository(_context));
        var tireServiceService = new TireServicesService(tireServiceRepository, servicePriceService);
        var newTireService = tireServiceService.CreateTireService("Test service", 2500);

        // Act
        var result = tireServiceService.GetTireServiceById(newTireService.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newTireService.Id, result.Id);
    }
    [Fact]
    public void GetTireServiceById_Should_NotRetrieveTireService_When_TireServiceDoesNotExist_Return_Null()
    {
        // Arrange
        var tireServiceRepository = new TireServicesRepository(_context);
        var servicePriceService = new ServicePriceService(new ServicePriceRepository(_context));
        var tireServiceService = new TireServicesService(tireServiceRepository, servicePriceService);

        // Act
        var result = tireServiceService.GetTireServiceById(999); 

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void UpdateTireServices_Should_UpdateTireService_Return_UpdatedTireService()
    {
        // Arrange
        var tireServiceRepository = new TireServicesRepository(_context);
        var servicePriceService = new ServicePriceService(new ServicePriceRepository(_context));
        var tireServiceService = new TireServicesService(tireServiceRepository, servicePriceService);
        var tireServiceToUpdate = tireServiceService.CreateTireService("Test service", 2500);
        tireServiceToUpdate.ServiceName = "Updated service";

        // Act
        var result = tireServiceService.UpdateTireServices(tireServiceToUpdate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated service", result.ServiceName);
    }

    [Fact]
    public void DeleteTireService_Should_DeleteTireService()
    {
        // Arrange
        var tireServiceRepository = new TireServicesRepository(_context);
        var servicePriceService = new ServicePriceService(new ServicePriceRepository(_context));
        var tireServiceService = new TireServicesService(tireServiceRepository, servicePriceService);
        var tireServiceToDelete = tireServiceService.CreateTireService("Test service", 2500);

        // Act
        tireServiceService.DeleteTireService(tireServiceToDelete.Id);

        // Assert
        var deletedTireService = tireServiceService.GetTireServiceById(tireServiceToDelete.Id);
        Assert.Null(deletedTireService);
    }

    [Fact]
    public void DeleteTireService_ShouldNot_DeleteTireService()
    {
        // Arrange
        var tireServiceRepository = new TireServicesRepository(_context);
        var servicePriceService = new ServicePriceService(new ServicePriceRepository(_context));
        var tireServiceService = new TireServicesService(tireServiceRepository, servicePriceService);
        var tireServiceToDelete = tireServiceService.CreateTireService("Test service", 2500);

        // Act
        tireServiceService.DeleteTireService(tireServiceToDelete.Id + 1); 

        // Assert
        var deletedTireService = tireServiceService.GetTireServiceById(tireServiceToDelete.Id);
        Assert.NotNull(deletedTireService);
    }

}
