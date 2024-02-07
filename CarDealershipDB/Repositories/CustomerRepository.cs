

using CarDealershipDB.Context;
using CarDealershipDB.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CarDealershipDB.Repositories;

public class CustomerRepository(DataContext context) : Repo<CustomerEntity>(context)
{
    public override CustomerEntity Get(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            var entity = _context.Set<CustomerEntity>()
                                .Include(p => p.Address)
                                .Include(p => p.Product)
                                .ThenInclude(p => p.Category)
                                .Include(p => p.Product)
                                .ThenInclude(p => p.Price)
                                .FirstOrDefault(expression);
            return entity!;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
