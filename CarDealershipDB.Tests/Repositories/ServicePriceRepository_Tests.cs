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
    public class ServicePriceRepository_Tests
    {
        private AppDBContext _context;
        private ServicePriceRepository _repo;

        public ServicePriceRepository_Tests()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
            _context = new AppDBContext(options);
            _repo = new ServicePriceRepository(_context);
        }

        [Fact]
        public void Create_Should_Create_ServicePrice_To_Database_Then_Return_ServicePrice_WithId1()
        {
            // Arrange

            var servicePrice = new ServicePrice {Cost = 100 };

            // Act
            var result = _repo.Create(servicePrice);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

        }

        [Fact]
        public void Create_Should_Not_CreateServicePriceIfExists_ReturnServicePrice()
        {
            // Arrange
            var servicePriceRepository = new ServicePriceRepository(_context);
            var servicePrice = new ServicePrice();

            // Act
            var result = servicePriceRepository.Create(servicePrice);

            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void Get_Retrieves_AllRecords_From_ServicePrice_Returns_IEnumerableofTypeServicePrice()
        {
            // Arrange
            var servicePriceRepository = new ServicePriceRepository(_context);

            // Act
            var result = _repo.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ServicePrice>>(result);
        }

        [Fact]
        public void Get_ShouldRetrieve_OneCostByCategoryId_Return_One_Cost()
        {
            // Arrange
            var servicePriceRepository = new ServicePriceRepository(_context);
            var servicePrice = new ServicePrice { Cost = 100 };
            _repo.Create(servicePrice);


            // Act
            var result = _repo.Get(x => x.Id == servicePrice.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(servicePrice.Id, result.Id);
        }

        [Fact]
        public void Get_ShouldNotFind_OneCostById_Return_Null()
        {
            // Arrange
            var servicePriceRepository = new ServicePriceRepository(_context);
            var servicePrice = new ServicePrice { Cost = 100 };

            // Act
            var result = _repo.Get(x => x.Id == servicePrice.Id);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public void Update_ShouldUpdateExistingCost_ReturnUpdatedCost()
        {
            // Arrange
            var servicePriceRepository = new ServicePriceRepository(_context);
            var servicePrice = new ServicePrice { Cost = 1 };
            servicePrice = _repo.Create(servicePrice);


            // Act
            servicePrice.Cost = 1;
            var result = _repo.Update(x => x.Id == servicePrice.Id, servicePrice);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(servicePrice.Id, result.Id);
            Assert.Equal(1, result.Id);


        }


        [Fact]
        public void Delete_ShouldRemoveOneCost_Return_True()
        {
            // Arrange
            var servicePriceRepository = new ServicePriceRepository(_context);
            var servicePrice = new ServicePrice { Cost = 100 };
            servicePrice = _repo.Create(servicePrice);

            // Act
            var result = _repo.Delete(x => x.Id == servicePrice.Id);

            // Assert

            Assert.True(result);
        }
        [Fact]
        public void Delete_ShouldNotFindCostAndRemoveIt_Return_False()
        {
            // Arrange
            var servicePriceRepository = new ServicePriceRepository(_context);
            var servicePrice = new ServicePrice { Cost = 100 };

            // Act
            var result = _repo.Delete(x => x.Id == servicePrice.Id);

            // Assert

            Assert.False(result);
        }

    }
}
