using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.Repositories;

public class TireInventoryRepository(ApplicationDBContext context) : GeneralRepo<TireInventory>(context)
{

}
