using CarDealershipDB.Context;
using CarDealershipDB.Repositories;
using CarDealershipDB.Service;
using CarDealershipDB.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=LAPTOP-R9TIM88L;Initial Catalog=CustomerDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True"));
            services.AddDbContext<AppDBContext>(x => x.UseSqlServer(@"Data Source=LAPTOP-r9tim88l;Initial Catalog=ProdCatalog;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));
          
            services.AddScoped<AddressRepository>();
            services.AddScoped<CategoryRepository>();
            services.AddScoped<CustomerRepository>();
            services.AddScoped<PriceRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<PricesRepository>();
            services.AddScoped<ServicePriceRepository>();
            services.AddScoped<TireInventoryRepository>();
            services.AddScoped<TiresRepository>();
            services.AddScoped<TireServicesRepository>();

         
            services.AddScoped<AddressService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<PriceService>();
            services.AddScoped<ProductService>();
            services.AddScoped<PricesService>();
            services.AddScoped<ServicePriceService>();
            services.AddScoped<TireInventoryService>();
            services.AddScoped<TiresService>();
            services.AddScoped<TireServicesService>();

            services.AddSingleton<MenuService>();

        }).Build();
      

        var productRepository = builder.Services.GetRequiredService<ProductRepository>();
        var categoryService = builder.Services.GetRequiredService<CategoryService>();
        var priceService = builder.Services.GetRequiredService<PriceService>();
        var pricesRepository = builder.Services.GetRequiredService<PricesRepository>();
        var servicePriceRepository = builder.Services.GetRequiredService<ServicePriceRepository>();
        var tireInventoryRepository = builder.Services.GetRequiredService<TireInventoryRepository>();
        var tiresRepository = builder.Services.GetRequiredService<TiresRepository>();
        var tireServicesRepository = builder.Services.GetRequiredService<TireServicesRepository>();

        var pricesService = builder.Services.GetRequiredService<PricesService>();
        var servicePriceService = builder.Services.GetRequiredService<ServicePriceService>();
        var tireInventoryService = builder.Services.GetRequiredService<TireInventoryService>();
        var tiresService = builder.Services.GetRequiredService<TiresService>();
        var tireServicesService = builder.Services.GetRequiredService<TireServicesService>();
        var customerRepository = builder.Services.GetRequiredService<CustomerRepository>();
        var addressService = builder.Services.GetRequiredService<AddressService>();

      
        var pricesServiceInstance = new PricesService(pricesRepository);
        var servicePriceServiceInstance = new ServicePriceService(servicePriceRepository);
        var tireInventoryServiceInstance = new TireInventoryService(tireInventoryRepository);
        var tiresServiceInstance = new TiresService(tiresRepository, pricesService, tireInventoryService);
        var tireServicesServiceInstance = new TireServicesService(tireServicesRepository, servicePriceService);
        var productService = new ProductService(productRepository, categoryService, priceService);
        var customerService = new CustomerService(customerRepository, addressService, productService);

        var app = new MenuService(productService, customerService, tiresService, tireServicesService);

        
        app.Menu();

    }
}


