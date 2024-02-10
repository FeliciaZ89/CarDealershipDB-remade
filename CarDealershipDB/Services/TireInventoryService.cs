using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CarDealershipDB.Services
{
    public class TireInventoryService(TireInventoryRepository tireRepository)
    {
        private readonly TireInventoryRepository _tireRepository = tireRepository;

        public TireInventory CreateQuantity(int quantity)
        {
            try
            {

                var tireInventoryEntity = new TireInventory { Quantity = quantity };
                tireInventoryEntity = _tireRepository.Create(tireInventoryEntity);
                return tireInventoryEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }



        public TireInventory GetQuantityById(int id)
        {
            try
            {
                var tireInventoryEntity = _tireRepository.Get(x => x.Id == id);
                return tireInventoryEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;

        }

        public IEnumerable<TireInventory> GetQuantities()
        {
            try
            {
                var quantities = _tireRepository.GetAll();
                return quantities;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public TireInventory UpdateQuantity(TireInventory tireInventoryEntity)
        {
            try
            {
                var updatedTireInventoryEntity = _tireRepository.Update(x => x.Id == tireInventoryEntity.Id, tireInventoryEntity);
                return updatedTireInventoryEntity;
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            return null!;
        }

        public void DeleteQuantity(int id)
        {
            try
            {
                _tireRepository.Delete(x => x.Id == id);
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }

        }
    }
}
