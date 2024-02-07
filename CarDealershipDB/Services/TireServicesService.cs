using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using CarDealershipDB.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public class TireServicesService(TireServicesRepository tireserviceRepository, ServicePriceService servicepriceService)
{
    private readonly TireServicesRepository _tireserviceRepository = tireserviceRepository;
    private readonly ServicePriceService _servicepriceService = servicepriceService;


    public TireService CreateTireService(string serviceName, decimal cost)
    {
        try
        {
            var servicepricesEntity = _servicepriceService.CreateCost(cost);


            var tireservicesEntity = new TireService
            {
                ServiceName = serviceName,

                CostId = servicepricesEntity.Id

            };



            tireservicesEntity = _tireserviceRepository.Create(tireservicesEntity);
            return tireservicesEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }



    public TireService GetTireServiceById(int id)
    {
        try
        {
            var tireservicesEntity = _tireserviceRepository.Get(x => x.Id == id);
            return tireservicesEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;

    }

    public IEnumerable<TireService> GetTireServices()
    {
        try
        {
            var tireServices = _tireserviceRepository.GetAll()
                                                .Include(p => p.Cost)
                                                .ToList();

            return tireServices;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public TireService UpdateTireServices(TireService tireservicesEntity)
    {
        try
        {
            var updatedTireServicesEntity = _tireserviceRepository.Update(x => x.Id == tireservicesEntity.Id, tireservicesEntity);
            return updatedTireServicesEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public void DeleteTireService(int id)
    {
        try
        {
            _tireserviceRepository.Delete(x => x.Id == id);
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }


    }
}

