using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.Tests.Repositories
{
    public class TestPriceRepository : PriceRepository
    {
        public TestPriceRepository(DataContext context) : base(context)
        {
        }

        public override PriceEntity Create(PriceEntity priceEntity)
        {
            return null!;
        }
    }

}
