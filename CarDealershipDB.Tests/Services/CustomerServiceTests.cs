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

public class CustomerServiceTests
{
    private readonly DataContext _context =
        new(new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public void CreateCustomer_Should_CreateNewCustomer_ReturnCustomer()
    {
        // Arrange
        var customerRepository = new CustomerRepository(_context);
        var addressService = new AddressService(new AddressRepository(_context));
        var productService = new ProductService(new ProductRepository(_context), new CategoryService(new CategoryRepository(_context)), new PriceService(new PriceRepository(_context)));
        var customerService = new CustomerService(customerRepository, addressService, productService);

        // Act
        var result = customerService.CreateCustomer("Test", "User", "test.user@example.com", "1234567890", "Test Street", "123456", "Test City", "Test Make", "Test Model", 2022, "Test Category", 10000);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.FirstName);
        Assert.Equal("User", result.LastName);
        Assert.Equal("test.user@example.com", result.Email);
    }

    [Fact]
    public void CreateCustomer_ShouldNotCreateCustomer_When_EmailAlreadyExists_ReturnNull()
    {
        // Arrange
        var customerRepository = new CustomerRepository(_context);
        var addressService = new AddressService(new AddressRepository(_context));
        var productService = new ProductService(new ProductRepository(_context), new CategoryService(new CategoryRepository(_context)), new PriceService(new PriceRepository(_context)));
        var customerService = new CustomerService(customerRepository, addressService, productService);

     
        customerService.CreateCustomer("Test", "User", "test.user@example.com", "1234567890", "Test Street", "123456", "Test City", "Test Make", "Test Model", 2022, "Test Category", 10000);

        // Act
    
        var result = customerService.CreateCustomer("Another", "User", "test.user@example.com", "0987654321", "Another Street", "654321", "Another City", "Another Make", "Another Model", 2023, "Another Category", 20000);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetCustomer_Should_ReturnAllCustomers_From_Database_Returns_IEnumerableofTypeCustomerEntity()
    {
        // Arrange
        var customerRepository = new CustomerRepository(_context);
        var addressService = new AddressService(new AddressRepository(_context));
        var productService = new ProductService(new ProductRepository(_context), new CategoryService(new CategoryRepository(_context)), new PriceService(new PriceRepository(_context)));
        var customerService = new CustomerService(customerRepository, addressService, productService);
        // Act

        var result = customerService.GetCustomers();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<CustomerEntity>>(result);
    }

    [Fact]
    public void GetCustomerById_Should_RetrieveOneCustomer_Return_OneCustomer()
    {
        // Arrange
        var customerRepository = new CustomerRepository(_context);
        var addressService = new AddressService(new AddressRepository(_context));
        var productService = new ProductService(new ProductRepository(_context), new CategoryService(new CategoryRepository(_context)), new PriceService(new PriceRepository(_context)));
        var customerService = new CustomerService(customerRepository, addressService, productService);
        var newCustomer = customerService.CreateCustomer("Test", "User", "test.user@example.com", "1234567890", "Test Street", "123456", "Test City", "Test Make", "Test Model", 2022, "Test Category", 10000);


        // Act
        var result = customerService.GetCustomerById(1);

        // Assert
        Assert.NotNull(result);
   
    }

    [Fact]
    public void GetCustomerById_ShouldNotRetrieveCustomer_When_CustomerDoesNotExist_ReturnNull()
    {
        // Arrange
        var customerRepository = new CustomerRepository(_context);
        var addressService = new AddressService(new AddressRepository(_context));
        var productService = new ProductService(new ProductRepository(_context), new CategoryService(new CategoryRepository(_context)), new PriceService(new PriceRepository(_context)));
        var customerService = new CustomerService(customerRepository, addressService, productService);

        // Act
        var result = customerService.GetCustomerById(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void UpdateCustomer_Should_UpdateCustomer_Return_UpdatedCustomer()
    {
        // Arrange
        var customerRepository = new CustomerRepository(_context);
        var addressService = new AddressService(new AddressRepository(_context));
        var productService = new ProductService(new ProductRepository(_context), new CategoryService(new CategoryRepository(_context)), new PriceService(new PriceRepository(_context)));
        var customerService = new CustomerService(customerRepository, addressService, productService);
        var newCustomer = customerService.CreateCustomer("Test", "User", "test.user@example.com", "1234567890", "Test Street", "123456", "Test City", "Test Make", "Test Model", 2022, "Test Category", 10000); 
        newCustomer.FirstName = "Updated FirstName";
        newCustomer.LastName = "Updated LastName";
        newCustomer.Email = "Updated Email";
        newCustomer.PhoneNumber = "Updated phone number";
        newCustomer.Address.StreetName = "Updated street name";
        newCustomer.Address.PostalCode = "Updated postal code";
        newCustomer.Address.City = "Updated city";
        newCustomer.Product.Make = "Updated make";
        newCustomer.Product.Model = "Updated model";
        newCustomer.Product.Year = int.Parse("2023");
        newCustomer.Product.Category.CategoryName = "Updated category name";
        newCustomer.Product.Price.SellingPrice = decimal.Parse("11000");


        // Act
        var result = customerService.UpdateCustomer(newCustomer);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated FirstName", result.FirstName);
        Assert.Equal("Updated LastName", result.LastName);
        Assert.Equal("Updated Email", result.Email);
        Assert.Equal("Updated phone number", result.PhoneNumber);
        Assert.Equal("Updated street name", result.Address.StreetName);
        Assert.Equal("Updated postal code", result.Address.PostalCode);
        Assert.Equal("Updated city", result.Address.City);
        Assert.Equal("Updated make", result.Product.Make);
        Assert.Equal("Updated model", result.Product.Model);
        Assert.Equal(int.Parse("2023"), result.Product.Year);
        Assert.Equal("Updated category name", result.Product.Category.CategoryName);
        Assert.Equal(decimal.Parse("11000"), result.Product.Price.SellingPrice);

    }

    [Fact]
    public void DeleteCustomer_Should_FindCustomerandDeleteIt_ReturnTrue()
    {
        // Arrange
        var customerRepository = new CustomerRepository(_context);
        var addressService = new AddressService(new AddressRepository(_context));
        var productService = new ProductService(new ProductRepository(_context), new CategoryService(new CategoryRepository(_context)), new PriceService(new PriceRepository(_context)));
        var customerService = new CustomerService(customerRepository, addressService, productService);
        var newCustomer = customerService.CreateCustomer("Test", "User", "test.user@example.com", "1234567890", "Test Street", "123456", "Test City", "Test Make", "Test Model", 2022, "Test Category", 10000);

        // Act
        customerService.DeleteCustomer(newCustomer.Id);

        // Assert
        var result = customerService.GetCustomerById(newCustomer.Id);
        Assert.Null(result);
    }

    [Fact]
    public void DeleteCustomer_ShouldNotFindCustomerandDeleteIt_ReturnFalse()
    {
        // Arrange
        var customerRepository = new CustomerRepository(_context);
        var addressService = new AddressService(new AddressRepository(_context));
        var productService = new ProductService(new ProductRepository(_context), new CategoryService(new CategoryRepository(_context)), new PriceService(new PriceRepository(_context)));
        var customerService = new CustomerService(customerRepository, addressService, productService);
        var newCustomer = customerService.CreateCustomer("Test", "User", "test.user@example.com", "1234567890", "Test Street", "123456", "Test City", "Test Make", "Test Model", 2022, "Test Category", 10000);

        // Act
        customerService.DeleteCustomer(999);

        // Assert
        var result = customerService.GetCustomerById(999);
        Assert.Null(result);
    }



}
