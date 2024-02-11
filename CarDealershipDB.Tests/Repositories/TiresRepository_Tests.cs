using Xunit;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CarDealershipDB.Tests.Repositories
{
    public class TiresRepository_Tests
    {
        private ApplicationDBContext _context;
        private TiresRepository _repo;

        public TiresRepository_Tests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDBContext(options);
            _repo = new TiresRepository(_context);
        }

        [Fact]
        public void Create_Should_Create_Tire_To_Database_Then_Return_Tire_WithId1()
        {
            // Arrange
            var tire = new Tire { Brand = "Brand1", Size = "Size1", Type = "Type1", Seasonality = "Seasonality1", PriceId = 1, TireInventoryId = 1 };

            // Act
            var result = _repo.Create(tire);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
        }
        [Fact]
        public void Create_ShouldNot_Create_Tire_IfAlreadyExists_ReturnTire()
        {
            // Arrange
            var existingTire = _repo.Create(new Tire { Brand = "Brand1", Size = "Size1", Type = "Type1", Seasonality = "Seasonality1", PriceId = 1, TireInventoryId = 1 });

            // Act
            var result = _repo.Create(new Tire { Brand = "Brand1", Size = "Size1", Type = "Type1", Seasonality = "Seasonality1", PriceId = 1, TireInventoryId = 1 });

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_Retrieves_AllRecords_From_Tire_Returns_IEnumerableofTypeTire()
        {
            // Arrange

            // Act
            var result = _repo.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Tire>>(result);
        }

       

        [Fact]
        public void Get_ShouldNotFind_OneTireById_Return_Null()
        {
            // Arrange
            var tire = new Tire { Brand = "Brand1", Size = "Size1", Type = "Type1", Seasonality = "Seasonality1", PriceId = 1, TireInventoryId = 1 };

            // Act
            var result = _repo.Get(x => x.Id == tire.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Update_ShouldUpdateExistingTire_ReturnUpdatedTire()
        {
            // Arrange
            var tire = new Tire { Brand = "Brand1", Size = "Size1", Type = "Type1", Seasonality = "Seasonality1", PriceId = 1, TireInventoryId = 1 };
            tire = _repo.Create(tire);

            // Act
            tire.Brand = "Brand2";
            var result = _repo.Update(x => x.Id == tire.Id, tire);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tire.Id, result.Id);
            Assert.Equal("Brand2", result.Brand);
        }

        [Fact]
        public void Delete_ShouldRemoveOneTire_Return_True()
        {
            // Arrange
            var tire = new Tire { Brand = "Brand1", Size = "Size1", Type = "Type1", Seasonality = "Seasonality1", PriceId = 1, TireInventoryId = 1 };
            tire = _repo.Create(tire);

            // Act
            var result = _repo.Delete(x => x.Id == tire.Id);

            // Assert
            Assert.True(result);
        }

      
    }
}

