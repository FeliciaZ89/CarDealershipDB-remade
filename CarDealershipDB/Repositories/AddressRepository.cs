using CarDealershipDB.Context;
using CarDealershipDB.Entities;


namespace CarDealershipDB.Repositories;

    public class AddressRepository(DataContext context) : Repo<AddressEntity>(context)
    {
}

