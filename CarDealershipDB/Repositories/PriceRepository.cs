

using CarDealershipDB.Context;
using CarDealershipDB.Entities;


namespace CarDealershipDB.Repositories;

public class PriceRepository(DataContext context) : Repo<PriceEntity>(context)
{
}
