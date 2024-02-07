using CarDealershipDB.Entities;
using CarDealershipDB.Context;


namespace CarDealershipDB.Repositories;

public class ServicePriceRepository(AppDBContext context) : GeneralRepo<ServicePrice>(context)
{

}
