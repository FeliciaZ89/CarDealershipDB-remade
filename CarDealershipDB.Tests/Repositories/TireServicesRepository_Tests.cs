using Xunit;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CarDealershipDB.Tests.Repositories
{
    public class TireServicesRepository_Tests
    {
        private ApplicationDBContext _context;
        private TireServicesRepository _repo;

        public TireServicesRepository_Tests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDBContext(options);
            _repo = new TireServicesRepository(_context);
        }

        [Fact]
        public void Create_Should_Create_TireService_To_Database_Then_Return_TireService_WithId1()
        {
            // Arrange
            var tireService = new TireService { ServiceName = "Service1", CostId = 1 };

            // Act
            var result = _repo.Create(tireService);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
        }
        [Fact]
        public void Create_ShouldNot_Create_TireService_IfAlreadyExists_ReturnTireService()
        {
            // Arrange
            var existingTireService = _repo.Create(new TireService { ServiceName = "Service1", CostId = 1 });

            // Act
            var result = _repo.Create(new TireService { ServiceName = "Service1", CostId = 1 });

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAll_Retrieves_AllRecords_From_TireService_Returns_IEnumerableofTypeTireService()
        {
            // Arrange

            // Act
            var result = _repo.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<TireService>>(result);
        }
       

        [Fact]
        public void Get_ShouldNotFind_OneTireServiceById_Return_Null()
        {
            // Arrange
            var tireService = new TireService { ServiceName = "Service1", CostId = 1 };

            // Act
            var result = _repo.Get(x => x.Id == tireService.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Update_ShouldUpdateExistingTireService_ReturnUpdatedTireService()
        {
            // Arrange
            var tireService = new TireService { ServiceName = "Service1", CostId = 1 };
            tireService = _repo.Create(tireService);

            // Act
            tireService.ServiceName = "Service2";
            var result = _repo.Update(x => x.Id == tireService.Id, tireService);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tireService.Id, result.Id);
            Assert.Equal("Service2", result.ServiceName);
        }

        [Fact]
        public void Delete_ShouldRemoveOneTireService_Return_True()
        {
            // Arrange
            var tireService = new TireService { ServiceName = "Service1", CostId = 1 };
            tireService = _repo.Create(tireService);

            // Act
            var result = _repo.Delete(x => x.Id == tireService.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Delete_ShouldNotRemoveOneTireService_IfNotExists_Return_False()
        {
            // Arrange
            var nonExistingId = 999;

            // Act
            var result = _repo.Delete(x => x.Id == nonExistingId);

            // Assert
            Assert.False(result);
        }
    }

}
