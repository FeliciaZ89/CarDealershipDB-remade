

using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipDB.Tests.Repositories;

public class AddressRepository_Tests
{
    private DataContext _context;
    private AddressRepository _repo;

    public AddressRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
         .Options;
        _context = new DataContext(options);
        _repo = new AddressRepository(_context);
    }

    [Fact]
    public void Create_Should_Create_AddressEntity_To_Database_Then_Return_AddressEntity_WithId1()
    {
        // Arrange

        var addressEntity = new AddressEntity { StreetName = "Test Street", City="Test City", PostalCode="123456" };

        // Act
        var result = _repo.Create(addressEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Create_Should_Not_SaveRecord_To_AddressEntity_Return_Null()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressEntity = new AddressEntity();

        // Act
        var result = _repo.Create(addressEntity);

        // Assert
        Assert.Null(result);

    }


    [Fact]
    public void Get_Retrieves_AllRecords_From_AddressEntity_Returns_IEnumerableofTypeAddressEntity()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);

        // Act
        var result = _repo.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<AddressEntity>>(result);
    }

    [Fact]
    public void Get_ShouldRetrieve_OneAddressById_Return_One_Address()
    {
           // Arrange
           var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name
                .Options;
           var context = new DataContext(options);
           var repo = new Repo<AddressEntity>(context);
           var addressEntity = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" };

           // Act
           var createdEntity = repo.Create(addressEntity);
           Assert.NotNull(createdEntity); // Ensure the entity is created

           var result = repo.Get(x => x.Id == createdEntity.Id);

           // Assert
           Assert.NotNull(result);
           Assert.Equal(createdEntity.Id, result.Id);
     }

    [Fact]
    public void Get_ShouldNotFind_OneAddressById_Return_Null()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressEntity = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" };
    

        // Act
        var result = _repo.Get(x => x.Id == addressEntity.Id);

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void Update_ShouldUpdateExistingAddress_ReturnUpdatedAddress()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressEntity = new AddressEntity  { StreetName = "Test Street", City = "Test City", PostalCode = "123456" };
   
        addressEntity = _repo.Create(addressEntity);


        // Act
        addressEntity.StreetName = "Testing street";
        addressEntity.City = "Testing city";
        addressEntity.PostalCode = "222222";
        var result = _repo.Update(x => x.Id ==addressEntity.Id, addressEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(addressEntity.Id, result.Id);
        Assert.Equal("Testing street", result.StreetName);
        Assert.Equal("Testing city", result.City);
        Assert.Equal("222222", result.PostalCode);

     }
    [Fact]
    public void Update_ShouldNotUpdateNonexistentAddress_ReturnNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;
        var context = new DataContext(options);
        var repo = new Repo<AddressEntity>(context);
        var nonExistentAddress = new AddressEntity { Id = 999, StreetName = "Nonexistent Street", City = "Nonexistent City", PostalCode = "000000" };

        // Act
        var result = repo.Update(x => x.Id == nonExistentAddress.Id, nonExistentAddress);

        // Assert
        Assert.Null(result);
    }


    [Fact]
    public void Delete_ShouldRemoveOneAddress_Return_True()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressEntity = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" };
        _repo.Create(addressEntity);

        // Act
        var result = _repo.Delete(x => x.Id == addressEntity.Id);

        // Assert

        Assert.True(result);
    }
    [Fact]
    public void Delete_ShouldNotFindAddressAndRemoveIt_Return_False()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressEntity = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" };

        // Act
        var result = _repo.Delete(x => x.Id == addressEntity.Id);

        // Assert

        Assert.False(result);
    }




}
