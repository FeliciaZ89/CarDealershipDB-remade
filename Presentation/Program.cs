using CarDealershipDB.Context;
using CarDealershipDB.Repositories;
using CarDealershipDB.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Services;



var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=LAPTOP-R9TIM88L;Initial Catalog=CustomerDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True"));
   
    //REPO'S
    services.AddScoped<AddressRepository>();
    services.AddScoped<CategoryRepository>();
    services.AddScoped<CustomerRepository>();
    services.AddScoped<PriceRepository>();
    services.AddScoped<ProductRepository>();

    //SERVICES
    services.AddScoped<AddressService>();
    services.AddScoped<CategoryService>();
    services.AddScoped<CustomerService>();
    services.AddScoped<PriceService>();
    services.AddScoped<ProductService>();

    services.AddSingleton<MenuService>();

}).Build();

var menuService = builder.Services.GetRequiredService<MenuService>();
menuService.CreateProduct_Menu();
