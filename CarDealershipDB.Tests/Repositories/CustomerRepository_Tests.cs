using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.Tests.Repositories
{
    public class CustomerRepository_Tests
    {
        private DataContext _context;
        private CustomerRepository _repo;

        public CustomerRepository_Tests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            _context = new DataContext(options);
            _repo = new CustomerRepository(_context);
        }
        [Fact]
        public void Create_Should_Create_CustomerEntity_To_Database_Then_Return_CustomerEntity_WithId1()
        {
            // Arrange
            var addressEntity = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" };
            var productEntity = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } };
            var customerEntity = new CustomerEntity { FirstName = "Test First Name", LastName = "Test Last Name", Email = "test@example.com", PhoneNumber = "1234567890", Address = addressEntity, Product = productEntity };

            // Act
            var result = _repo.Create(customerEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Create_Should_Not_SaveRecord_To_CustomerEntity_Return_Null()
        {
            // Arrange
            var customerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity();

            // Act
            var result = _repo.Create(customerEntity);

            // Assert
            Assert.Null(result);
        }

        [Fact]// test for overriden method 
        public void Get_ShouldRetrieve_OneCustomerWithAddressAndProduct_Return_One_Customer()
        {
            // Arrange
            var addressEntity = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" };
            var productEntity = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } };
            var customerEntity = new CustomerEntity { FirstName = "Test First Name", LastName = "Test Last Name", Email = "test@example.com", PhoneNumber = "1234567890", Address = addressEntity, Product = productEntity };
            _repo.Create(customerEntity);

            // Act
            var result = _repo.Get(x => x.Id == customerEntity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerEntity.Id, result.Id);
            Assert.NotNull(result.Address);
            Assert.NotNull(result.Product);
            Assert.NotNull(result.Product.Category);
            Assert.NotNull(result.Product.Price);
        }

        [Fact]
            public void Get_Retrieves_AllRecords_From_CustomerEntity_Returns_IEnumerableofTypeCustomerEntity()
            {
                // Arrange

                // Act
                var result = _repo.GetAll();

                // Assert
                Assert.NotNull(result);
                Assert.IsAssignableFrom<IEnumerable<CustomerEntity>>(result);
            }

            [Fact]
            public void Get_ShouldRetrieve_OneCustomerById_Return_One_Customer()
            {
                // Arrange
                var customerEntity = new CustomerEntity { FirstName = "Test First Name", LastName = "Test Last Name", Email = "test@example.com", PhoneNumber = "1234567890", Address = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" }, Product = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } } };

                // Act
                var createdEntity = _repo.Create(customerEntity);
                Assert.NotNull(createdEntity); // Ensure the entity is created

                var result = _repo.Get(x => x.Id == createdEntity.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(createdEntity.Id, result.Id);
            }

            [Fact]
            public void Get_ShouldNotFind_OneCustomerById_Return_Null()
            {
                // Arrange
                var customerEntity = new CustomerEntity { FirstName = "Test First Name", LastName = "Test Last Name", Email = "test@example.com", PhoneNumber = "1234567890", Address = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" }, Product = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } } };

                // Act
                var result = _repo.Get(x => x.Id == customerEntity.Id);

                // Assert
                Assert.Null(result);
            }

            [Fact]
            public void Update_ShouldUpdateExistingCustomer_ReturnUpdatedCustomer()
            {
                // Arrange
                var customerEntity = new CustomerEntity { FirstName = "Test First Name", LastName = "Test Last Name", Email = "test@example.com", PhoneNumber = "1234567890", Address = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" }, Product = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity {CategoryName = "Test Category" } } };
                customerEntity = _repo.Create(customerEntity);

                // Act
                customerEntity.FirstName = "Testing First Name";
                customerEntity.LastName = "Testing Last Name";
                customerEntity.Email = "testing@example.com";
                customerEntity.PhoneNumber = "0987654321";
                customerEntity.Address.StreetName = "Testing Street";
                customerEntity.Address.City = "Testing City";
                customerEntity.Address.PostalCode = "654321";
                customerEntity.Product.Make = "Testing Make";
                customerEntity.Product.Model = "Testing Model";
                customerEntity.Product.Year = 2023;
                customerEntity.Product.Price.SellingPrice = 200;
                customerEntity.Product.Category.CategoryName = "Testing Category";
                var result = _repo.Update(x => x.Id == customerEntity.Id, customerEntity);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(customerEntity.Id, result.Id);
                Assert.Equal("Testing First Name", result.FirstName);
                Assert.Equal("Testing Last Name", result.LastName);
                Assert.Equal("testing@example.com", result.Email);
                Assert.Equal("0987654321", result.PhoneNumber);
                Assert.Equal("Testing Street", result.Address.StreetName);
                Assert.Equal("Testing City", result.Address.City);
                Assert.Equal("654321", result.Address.PostalCode);
                Assert.Equal("Testing Make", result.Product.Make);
                Assert.Equal("Testing Model", result.Product.Model);
                Assert.Equal(2023, result.Product.Year);
                Assert.Equal(200, result.Product.Price.SellingPrice);
                Assert.Equal("Testing Category", result.Product.Category.CategoryName);
            }

            [Fact]
            public void Update_ShouldNotUpdateNonexistentCustomer_ReturnNull()
            {
                // Arrange
                var nonExistentCustomer = new CustomerEntity { Id = 999, FirstName = "Nonexistent First Name", LastName = "Nonexistent Last Name", Email = "nonexistent@example.com", PhoneNumber = "0000000000", Address = new AddressEntity { StreetName = "Nonexistent Street", City = "Nonexistent City", PostalCode = "000000" }, Product = new ProductEntity { Make = "Nonexistent Make", Model = "Nonexistent Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Nonexistent Category" } } };

                // Act
                var result = _repo.Update(x => x.Id == nonExistentCustomer.Id, nonExistentCustomer);

                // Assert
                Assert.Null(result);
            }

            [Fact]
            public void Delete_ShouldRemoveOneCustomer_Return_True()
            {
                // Arrange
                var customerEntity = new CustomerEntity { FirstName = "Test First Name", LastName = "Test Last Name", Email = "test@example.com", PhoneNumber = "1234567890", Address = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" }, Product = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } } };
                _repo.Create(customerEntity);

                // Act
                var result = _repo.Delete(x => x.Id == customerEntity.Id);

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Delete_ShouldNotFindCustomerAndRemoveIt_Return_False()
            {
                // Arrange
                var customerEntity = new CustomerEntity { FirstName = "Test First Name", LastName = "Test Last Name", Email = "test@example.com", PhoneNumber = "1234567890", Address = new AddressEntity { StreetName = "Test Street", City = "Test City", PostalCode = "123456" }, Product = new ProductEntity { Make = "Test Make", Model = "Test Model", Year = 2022, Price = new PriceEntity { SellingPrice = 100 }, Category = new CategoryEntity { CategoryName = "Test Category" } } };

                // Act
                var result = _repo.Delete(x => x.Id == customerEntity.Id);

                // Assert
                Assert.False(result);
            }
        


    }
}
