using Xunit;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarDealershipDB.Tests.Repositories
{
    public class PricesRepository_Tests
    {
        private ApplicationDBContext _context;
        private PricesRepository _repo;

        public PricesRepository_Tests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDBContext(options);
            _repo = new PricesRepository(_context);
        }

        [Fact]
        public void Create_Should_Create_Price_To_Database_Then_Return_Price_WithId1()
        {
            // Arrange
            var price = new Price { Price1 = 100 };

            // Act
            var result = _repo.Create(price);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0); 
        }

        [Fact]
        public void Create_Should_Not_CreatePriceIfExists_To_Returnprice()
        {
            // Arrange
            var priceRepository = new PricesRepository(_context);
            var price = new Price();

            // Act
            var result = priceRepository.Create(price);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_Retrieves_AllRecords_From_Price_Returns_IEnumerableofTypePrice()
        {
            // Arrange
            var priceRepository = new PricesRepository(_context) ;

            // Act
            var result = _repo.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Price>>(result);
        }

        [Fact]
        public void Get_ShouldRetrieve_OnePriceById_Return_One_Price()
        {
            // Arrange
            var priceRepository = new PricesRepository(_context);
            var price = new Price { Price1=100};
            _repo.Create(price);

            // Act
            var result = _repo.Get(x => x.Id == price.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(price.Id, result.Id);
        }
        [Fact]
        public void Get_ShouldNotFind_OnePriceById_Return_Null()
        {
            // Arrange
            var priceRepository = new PricesRepository(_context);
            var price = new Price { Price1 = 100 };

            // Act
            var result = _repo.Get(x => x.Id == price.Id);

            // Assert
            Assert.Null(result);
        }
            [Fact]
        public void Update_ShouldUpdateExistingPrice_ReturnUpdatedPrice()
        {
            // Arrange
            var priceRepository = new PricesRepository(_context);
            var price = new Price {Price1 = 1 };
            price = _repo.Create(price);

            // Act
            price.Price1 = 1;
            var result = _repo.Update(x => x.Id == price.Id, price);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(price.Id, result.Id);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Delete_ShouldRemoveOnePrice_Return_True()
        {
            // Arrange
            var priceRepository = new PricesRepository(_context);
            var price = new Price { Price1 = 150 };
            price = _repo.Create(price);

            // Act
            var result = _repo.Delete(x => x.Id == price.Id);

            // Assert
            Assert.True(result);
        }
       
  
        [Fact]
        public void Delete_ShouldNotFindPriceAndRemoveIt_Return_False()
        {
            // Arrange
            var priceRepository = new PricesRepository(_context);
            var price = new Price { Price1 = 100 };

            // Act
            var result = _repo.Delete(x => x.Id == price.Id);

            // Assert

            Assert.False(result);
        }
        
    }
}
