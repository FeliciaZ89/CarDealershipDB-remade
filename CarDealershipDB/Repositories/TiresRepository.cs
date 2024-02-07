using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Diagnostics;


namespace CarDealershipDB.Repositories;

public class TiresRepository(AppDBContext context) : GeneralRepo<Tire>(context)
{
    public override Tire Get(Expression<Func<Tire, bool>> expression)
    {
        try
        {
            var entity = _context.Set<Tire>()
                                .Include(p => p.Price)
                                .Include(p => p.TireInventory)

                                .FirstOrDefault(expression);
            return entity!;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

}
