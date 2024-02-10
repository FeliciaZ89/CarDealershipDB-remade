using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

public class TiresRepository : GeneralRepo<Tire>
{
    public TiresRepository(AppDBContext context) : base(context) { }

    public override Tire Get(Expression<Func<Tire, bool>> expression)
    {
        try
        {
            var entity = _context.Set<Tire>()
                                .Include(t => t.Price)
                                .Include(t => t.TireInventory)
                                .FirstOrDefault(expression);
            return entity!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            throw; 
        }
    }
}
