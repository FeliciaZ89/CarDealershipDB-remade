using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.Repositories;

public class TireInventoryRepository(AppDBContext context) : GeneralRepo<TireInventory>(context)
{

}
