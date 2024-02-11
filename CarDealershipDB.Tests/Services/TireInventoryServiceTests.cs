using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Services;
using Microsoft.EntityFrameworkCore;
using System;

public class TireInventoryServiceTests
{
    private readonly ApplicationDBContext _context =
        new(new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public void CreateQuantity_Should_CreateNewTireInventory_ReturnTireInventory()
    {
        // Arrange
        var tireInventoryRepository = new TireInventoryRepository(_context);
        var tireInventoryService = new TireInventoryService(tireInventoryRepository);
        int quantity = 100;

        // Act
        var result = tireInventoryService.CreateQuantity(quantity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(quantity, result.Quantity);
    }

    [Fact]
    public void GetAllQuantities_Should_ReturnAllTireInventories_From_Database_Returns_IEnumerableofTypeTireInventory()
    {
        // Arrange
        var tireInventoryRepository = new TireInventoryRepository(_context);
        var tireInventoryService = new TireInventoryService(tireInventoryRepository);

        // Act
       
         var result = tireInventoryService.GetQuantities();
     

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<TireInventory>>(result);
    }

    [Fact]
    public void GetQuantityById_Should_RetrieveOneTireInventory_ReturnOneTireInventory()
    {
        // Arrange
        var tireRepository = new TireInventoryRepository(_context);
        var tireService = new TireInventoryService(tireRepository);
        var newTire = tireService.CreateQuantity(10);

        // Act
        var result = tireService.GetQuantityById(newTire.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newTire.Id, result.Id);
    }

    [Fact]
    public void GetQuantityById_ShouldNot_RetrieveOneTireInventoryWhenNotExists_ReturnNull()
    {
        // Arrange
        var tireRepository = new TireInventoryRepository(_context);
        var tireService = new TireInventoryService(tireRepository);

        // Act
        var result = tireService.GetQuantityById(10);

        // Assert
        Assert.Null(result);
      
    }
    [Fact]
    public void UpdateQuantity_Should_UpdateQuantity_Return_UpdatedQuantity()
    {
        // Arrange
        var tireRepository = new TireInventoryRepository(_context);
        var tireService = new TireInventoryService(tireRepository);
        var newQuantity = tireService.CreateQuantity(10);

        newQuantity.Quantity =11;
       

        var updatedQuantity = tireService.UpdateQuantity(newQuantity);

        // Assert
        Assert.NotNull(updatedQuantity);
        Assert.Equal(11, updatedQuantity.Quantity);
    }

    [Fact]
    public void DeleteQuantitiy_Should_FindQuantitiyandDeleteIt_ReturnTrue()
    {
        // Arrange
        var tireRepository = new TireInventoryRepository(_context);
        var tireService = new TireInventoryService(tireRepository);
        var newQuantity = tireService.CreateQuantity(10);


        // Act
        tireService.DeleteQuantity(newQuantity.Id);

        // Assert
        var result =tireService.GetQuantityById(newQuantity.Id);
        Assert.Null(result);
    }

    [Fact]
    public void DeleteQuantitiy_ShouldNot_FindQuantitiyandDeleteIt()
    {
        // Arrange
        var tireRepository = new TireInventoryRepository(_context);
        var tireService = new TireInventoryService(tireRepository);
        var newQuantity = tireService.CreateQuantity(10);


        // Act
        tireService.DeleteQuantity(999);

        // Assert
        var result = tireService.GetQuantityById(999);
        Assert.Null(result);
    }

}




