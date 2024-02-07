
using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarDealershipDB.Repositories;

public class TireServicesRepository(AppDBContext context) : GeneralRepo<TireService>(context)
{
    public override TireService Get(Expression<Func<TireService, bool>> expression)
    {
        try
        {
            var entity = _context.Set<TireService>()
                                .Include(p => p.Cost)

                                .FirstOrDefault(expression);
            return entity!;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

}



