using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CarDealershipDB.Services
{
    public class PricesService(PricesRepository pricesRepository)
    {
        private readonly PricesRepository _pricesRepository = pricesRepository;

        public Price CreatePrice(decimal price1)
        {
         
            try
            {
               
                    var pricesEntity = new Price { Price1 = price1 };
                    pricesEntity = _pricesRepository.Create(pricesEntity);
                    return pricesEntity;
                
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }


        public Price GetPriceById(int id)
        {
            try
            {
                var pricesEntity = _pricesRepository.Get(x => x.Id == id);
                return pricesEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;

        }

        public IEnumerable<Price> GetPrices()
        {
            try
            {
                var prices = _pricesRepository.GetAll();
                return prices;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public Price UpdatePrice(Price pricesEntity)
        {
            try
            {
                var updatedPricesEntity = _pricesRepository.Update(x => x.Id == pricesEntity.Id, pricesEntity);
                return updatedPricesEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public void DeletePrice(int id)
        {
            try
            {
                _pricesRepository.Delete(x => x.Id == id);
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }

        }
    }
}

