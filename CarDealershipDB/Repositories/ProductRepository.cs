


using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CarDealershipDB.Repositories;

public class ProductRepository : Repo<ProductEntity>
{
    public ProductRepository(DataContext context) : base(context)
    {
    }

    public override ProductEntity Get(Expression<Func<ProductEntity, bool>> expression)
    {
        try
        {
            var entity = _context.Set<ProductEntity>()
                                 .Include(e => e.Price)
                                 .Include(e => e.Category)
                                 .FirstOrDefault(expression);
            return entity!;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }
}

