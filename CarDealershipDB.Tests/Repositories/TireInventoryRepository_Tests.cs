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
    public class TireInventoryRepository_Tests
    {
        private ApplicationDBContext _context;
        private TireInventoryRepository _repo;

        public TireInventoryRepository_Tests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            _context = new ApplicationDBContext(options);
            _repo = new TireInventoryRepository(_context);
        }

        [Fact]
        public void Create_Should_Create_TireInventoryEntity_To_Database_Then_Return_Quantity_WithId1()
        {
            // Arrange

            var tireInventory = new TireInventory { Quantity = 100 };

            // Act
            var result = _repo.Create(tireInventory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }

        [Fact]
        public void Create_Should_Not_CreateTireInventoryifExists_Return_TireInventory()
        {
            // Arrange
            var tireInventoryRepository = new TireInventoryRepository(_context);
            var tireInventory = new TireInventory();

            // Act
            var result = tireInventoryRepository.Create(tireInventory);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_Retrieves_AllRecords_From_TireInventory_Returns_IEnumerableOfTireInventory()
        {
            // Arrange
            var tireInventory1 = new TireInventory { Quantity = 10 };
            var tireInventory2 = new TireInventory { Quantity = 20 };
            _context.TireInventories.AddRange(tireInventory1, tireInventory2);
            _context.SaveChanges();

            // Act
            var result = _repo.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal(tireInventory1.Id, item.Id),
                item => Assert.Equal(tireInventory2.Id, item.Id));
        }

        [Fact]
        public void Get_ShouldRetrieve_OneTireInventoryById_Return_One_TireInventory()
        {
            // Arrange
            var tireInventory = new TireInventory { Quantity = 100 };
            _context.TireInventories.Add(tireInventory);
            _context.SaveChanges();

            // Act
            var result = _repo.Get(x => x.Id == tireInventory.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tireInventory.Id, result.Id);
        }

        [Fact]
        public void Update_ShouldUpdateExistingTireInventory_ReturnUpdatedTireInventory()
        {
            // Arrange
            var tireInventory = new TireInventory { Quantity = 100 };
            _context.TireInventories.Add(tireInventory);
            _context.SaveChanges();

            // Act
            tireInventory.Quantity = 200;
            var result = _repo.Update(x => x.Id == tireInventory.Id, tireInventory);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tireInventory.Id, result.Id);
            Assert.Equal(200, result.Quantity);
        }

        [Fact]
        public void Delete_ShouldRemoveOneTireInventory_Return_True()
        {
            // Arrange
            var tireInventory = new TireInventory { Quantity = 100 };
            _context.TireInventories.Add(tireInventory);
            _context.SaveChanges();

            // Act
            var result = _repo.Delete(x => x.Id == tireInventory.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_ShouldNotFindTireInventoryAndRemoveIt_Return_False()
        {
            // Arrange
            var tireInventory = new TireInventory { Quantity = 100 };

            // Act
            var result = _repo.Delete(x => x.Id == tireInventory.Id);

            // Assert
            Assert.False(result);
        }
    }
}

     
    

