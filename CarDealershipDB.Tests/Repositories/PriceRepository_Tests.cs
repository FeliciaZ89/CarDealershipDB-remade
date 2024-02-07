using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.Tests.Repositories
{
   public class PriceRepository_Tests
    {
        private DataContext _context;
        private PriceRepository _repo;

        public PriceRepository_Tests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            _context = new DataContext(options);
            _repo = new PriceRepository(_context);
        }

        [Fact]
        public void Create_Should_Create_PriceEntity_To_Database_Then_Return_PriceEntity_WithId1()
        {
            // Arrange

            var priceEntity = new PriceEntity { SellingPrice = 100 };

            // Act
            var result = _repo.Create(priceEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }

        [Fact]
        public void Create_Should_Not_SaveRecord_To_Database_Return_Null()
        {
            // Arrange
            var priceRepository = new TestPriceRepository(_context);
            var priceEntity = new PriceEntity();

            // Act
            var result = priceRepository.Create(priceEntity);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public void Get_Retrieves_AllRecords_From_PriceEntity_Returns_IEnumerableofTypePriceEntity()
        {
            // Arrange
            var priceRepository = new PriceRepository(_context);

            // Act
            var result = _repo.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PriceEntity>>(result);
        }

        [Fact]
        public void Get_ShouldRetrieve_OnePriceByCategoryId_Return_One_SellingPrice()
        {
            // Arrange
            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity { SellingPrice = 100 };
            _repo.Create(priceEntity);


            // Act
            var result = _repo.Get(x => x.Id == priceEntity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(priceEntity.Id, result.Id);
        }

        [Fact]
        public void Get_ShouldNotFind_OnePriceById_Return_Null()
        {
            // Arrange
            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity { SellingPrice =100 };

            // Act
            var result = _repo.Get(x => x.Id == priceEntity.Id);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public void Update_ShouldUpdateExistingSeelingPrice_ReturnUpdatedSellingPrice()
        {
            // Arrange
            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity { SellingPrice = 1 };
            priceEntity = _repo.Create(priceEntity);


            // Act
            priceEntity.SellingPrice = 1;
            var result = _repo.Update(x => x.Id == priceEntity.Id, priceEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(priceEntity.Id, result.Id);
            Assert.Equal(1, result.Id);


        }


        [Fact]
        public void Delete_ShouldRemoveOneSellingPrice_Return_True()
        {
            // Arrange
            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity {SellingPrice = 100 };
            _repo.Create(priceEntity);

            // Act
            var result = _repo.Delete(x => x.Id == priceEntity.Id);

            // Assert

            Assert.True(result);
        }
        [Fact]
        public void Delete_ShouldNotFindSellingPriceAndRemoveIt_Return_False()
        {
            // Arrange
            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity {SellingPrice = 100 };

            // Act
            var result = _repo.Delete(x => x.Id == priceEntity.Id);

            // Assert

            Assert.False(result);
        }

    }
}
