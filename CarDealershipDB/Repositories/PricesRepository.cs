using CarDealershipDB.Context;
using CarDealershipDB.Entities;


namespace CarDealershipDB.Repositories;

public class PricesRepository : GeneralRepo<Price>
{
    public PricesRepository(AppDBContext context) : base(context)
    {
    }

}

