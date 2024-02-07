using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CarDealershipDB.Services
{
    public class PriceService(PriceRepository priceRepository)
    {
        private readonly PriceRepository _priceRepository = priceRepository;

        public PriceEntity CreatePrice(decimal sellingPrice)
        {
            try
            {
                // Check if a price with the same selling price already exists
                var priceEntity = _priceRepository.Get(x => x.SellingPrice == sellingPrice);

                // If the price doesn't exist, create a new one
                priceEntity = _priceRepository.Create(new PriceEntity { SellingPrice = sellingPrice });

                return priceEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public PriceEntity GetPriceById(int id)
        {
            try
            {
                var priceEntity = _priceRepository.Get(x => x.Id == id);
                return priceEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;

        }

        public IEnumerable<PriceEntity> GetPrices()
        {
            try
            {
                var prices = _priceRepository.GetAll();
                return prices;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public PriceEntity UpdatePrice(PriceEntity priceEntity)
        {
            try
            {
                var updatedPriceEntity = _priceRepository.Update(x => x.Id == priceEntity.Id, priceEntity);
                return updatedPriceEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public void DeletePrice(int id)
        {
            try
            {
                _priceRepository.Delete(x => x.Id == id);
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
           
        }
    }
}
