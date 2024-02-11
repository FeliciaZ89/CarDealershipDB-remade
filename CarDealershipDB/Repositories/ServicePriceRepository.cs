using CarDealershipDB.Entities;
using CarDealershipDB.Context;


namespace CarDealershipDB.Repositories;

public class ServicePriceRepository(ApplicationDBContext context) : GeneralRepo<ServicePrice>(context)
{

}
