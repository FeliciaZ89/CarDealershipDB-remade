using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CarDealershipDB.Services
{
    public class ServicePriceService(ServicePriceRepository servicePriceRepository)
    {
        private readonly ServicePriceRepository _servicePriceRepository = servicePriceRepository;

        public ServicePrice CreateCost(decimal cost)
        {
            try
            {
                var servicePriceEntity = _servicePriceRepository.Get(x => x.Cost == cost);

              
                servicePriceEntity = _servicePriceRepository.Create(new ServicePrice{ Cost = cost });

                return servicePriceEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public ServicePrice GetCostById(int id)
        {
            try
            {
                var servicePriceEntity = _servicePriceRepository.Get(x => x.Id == id);
                return servicePriceEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;

        }

        public IEnumerable<ServicePrice> GetCosts()
        {
            try
            {
                var costs = _servicePriceRepository.GetAll();
                return costs;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public ServicePrice UpdateCost(ServicePrice servicePriceEntity)
        {
            try
            {
                var updatedServicePriceEntity = _servicePriceRepository.Update(x => x.Id == servicePriceEntity.Id, servicePriceEntity);
                return updatedServicePriceEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public void DeleteCost(int id)
        {
            try
            {
                _servicePriceRepository.Delete(x => x.Id == id);
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }

        }
    }
}

