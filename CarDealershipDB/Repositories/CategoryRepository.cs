using CarDealershipDB.Context;
using CarDealershipDB.Entities;



namespace CarDealershipDB.Repositories;
  public class CategoryRepository(DataContext context) : Repo<CategoryEntity>(context)
{
}
