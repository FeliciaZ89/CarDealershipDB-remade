using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CarDealershipDB.Services;

public class TiresService(TiresRepository tiresRepository, PricesService pricesService, TireInventoryService tireInventoryService)
{
    private readonly TiresRepository _tiresRepository = tiresRepository;
    private readonly PricesService _pricesService = pricesService;
    private readonly TireInventoryService _tireInventoryService = tireInventoryService;

    public Tire CreateTire(string brand, string size, string type, string seasonality, decimal price, int quantity)
    {
        try
        {
          
            var existingTire = _tiresRepository.Get(t => t.Brand == brand && t.Size == size && t.Type == type && t.Seasonality == seasonality && t.Price.Price1 == price);
            if (existingTire != null)
            {
                return null!;
            }
            var pricesEntity = _pricesService.CreatePrice(price);
            var tireInventoryEntity = _tireInventoryService.CreateQuantity(quantity);

            var tiresEntity = new Tire
            {
                Brand = brand,
                Type = type,
                Size = size,
                Seasonality = seasonality,
                PriceId = pricesEntity.Id,
                TireInventoryId = tireInventoryEntity.Id
            };

            tiresEntity = _tiresRepository.Create(tiresEntity);
            return tiresEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }



    public Tire GetTireById(int id)
    {
        try
        {
            var tiresEntity = _tiresRepository.Get(x => x.Id == id);
            return tiresEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;

    }

    public IEnumerable<Tire> GetTires()
    {
        try
        {
            var tires = _tiresRepository.GetAll()
                                                .Include(p => p.Price)
                                                .Include(p => p.TireInventory)

                                                .ToList();

            return tires;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public Tire UpdateTires(Tire tiresEntity)
    {
        try
        {
            var updatedTiresEntity = _tiresRepository.Update(x => x.Id == tiresEntity.Id, tiresEntity);
            return updatedTiresEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public void DeleteTires(int id)
    {
        try
        {
            _tiresRepository.Delete(x => x.Id == id);
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }


    }
}


