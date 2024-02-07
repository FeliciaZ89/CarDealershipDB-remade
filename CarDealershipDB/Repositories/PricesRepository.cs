
using CarDealershipDB.Entities;
using CarDealershipDB.Context;

namespace CarDealershipDB.Repositories;

public class PricesRepository(AppDBContext context) :GeneralRepo<Price>(context)
{

}
