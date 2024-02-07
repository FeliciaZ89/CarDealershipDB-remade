

using CarDealershipDB.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipDB.Context;

public  class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<PriceEntity> Prices { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
}
