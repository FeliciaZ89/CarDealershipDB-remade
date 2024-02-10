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

public class AddressServiceTests
{
    private readonly DataContext _context =
        new(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public void CreateAddress_Should_CreateNewAddress_ReturnAddress()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressService = new AddressService(addressRepository);
        string streetName = "Test Street";
        string postalCode = "123456";
        string city = "Test City";

        // Act
        var result = addressService.CreateAddress(streetName,postalCode,city);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(streetName, result.StreetName);
        Assert.Equal(postalCode, result.PostalCode);
        Assert.Equal(city, result.City);
    }


    [Fact]
    public void CreateAddress_ShouldNotCreateAddress_When_StreetNameIsNull_ReturnNull()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressService = new AddressService(addressRepository);
        string streetName = null!;
        string postalCode = "123456";
        string city = "Test City";

        // Act
        var result = addressService.CreateAddress(streetName, postalCode, city);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public void GetAddress_Should_ReturnAllAddresses_From_Database_Returns_IEnumerableofTypeAddressEntity()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressService = new AddressService(addressRepository);

        // Act
        var result = addressService.GetAddresses();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<AddressEntity>>(result);
    }


    [Fact]
    public void GetAddressById_Should_RetrieveOneAddress_Return_OneAddress()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressService = new AddressService(addressRepository);
        var newAddress = addressService.CreateAddress("Test Street","123456","Test city");

        // Act
        var result = addressService.GetAddresById(newAddress.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newAddress.Id, result.Id);
    }

    [Fact]
    public void GetAddressById_ShouldNotRetrieveAddress_When_AddresDoesNotExist_ReturnNull()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressService = new AddressService(addressRepository);
        // Act
        var result = addressService.GetAddresById(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
  
    public void UpdateAddress_Should_UpdateAddress_Return_UpdatedAddress()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressService = new AddressService(addressRepository);
        var newAddress = addressService.CreateAddress("Test Street", "123456", "Test City");
        newAddress.StreetName = "Updated StreetName";
        newAddress.PostalCode = "111111";
        newAddress.City = "Updated City";

        // Act
        var result = addressService.UpdateAddres(newAddress);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated StreetName", result.StreetName);
        Assert.Equal("111111", result.PostalCode);
        Assert.Equal("Updated City", result.City);
    }


    [Fact]
    public void DeleteAddress_Should_FindAddresandDeleteIt_ReturnTrue()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressService = new AddressService(addressRepository);
        var newAddress = addressService.CreateAddress("Test Street", "123456", "Test City");

        // Act
        addressService.DeleteAddres(newAddress.Id);

        // Assert
        var result = addressService.GetAddresById(newAddress.Id);
        Assert.Null(result);
    }

    [Fact]
    public void DeleteAddress_ShouldNotFindAddressandDeleteIt_ReturnFalse()
    {
        // Arrange
        var addressRepository = new AddressRepository(_context);
        var addressService = new AddressService(addressRepository);

        // Act
        addressService.DeleteAddres(999);

        // Assert
        var result = addressService.GetAddresById(999);
        Assert.Null(result);
    }
}



