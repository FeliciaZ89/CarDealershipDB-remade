using CarDealershipDB.Services;
using CarDealershipDB.Repositories;
using CarDealershipDB.Entities;



namespace CarDealershipDB.Service
{
    internal class MenuService(ProductService productService, CustomerService customerService, TiresService tiresService, TireServicesService tireServicesService)
    {
        private readonly ProductService _productService = productService;
        private readonly CustomerService _customerService = customerService;
        private readonly TiresService _tiresService = tiresService;
        private readonly TireServicesService _tireservicesService = tireServicesService;
        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("***** CAR DEALERSHIP MAIN MENU *****");
                Console.WriteLine("1. Product Services");
                Console.WriteLine("2. Customer Services");
                Console.WriteLine("3. Product Catalog");
                Console.WriteLine("4. Tires Services");
                Console.WriteLine("5. Exit");
                Console.Write("Please select an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ProductMenu();
                        break;
                    case "2":
                        CustomerMenu();
                        break;
                    case "3":
                        ProductCatalogMenu();
                        break;
                    case "4":
                        TireServices_Menu();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        public void ProductMenu()
        {
            Console.Clear();
            Console.WriteLine("***** PRODUCT SERVICES *****");
            Console.WriteLine("1. Add a new car to the database");
            Console.WriteLine("2. Get all cars from the database");
            Console.WriteLine("3. Get a car by id from the database");
            Console.WriteLine("4. Update selling price");
            Console.WriteLine("5. Delete car from the database");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Please select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateProduct_Menu();
                    break;
                case "2":
                    GetAllProducts_Menu();
                    break;
                case "3":
                    GetProductById_Menu();
                    break;
                case "4":
                    UpdateProduct_Menu();
                    break;
                case "5":
                    DeleteProduct_Menu();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        public void CreateProduct_Menu()
        {
            Console.Clear();
            Console.WriteLine("*****ADD NEW CAR *****");
            Console.Write("Car make:");
            var make = Console.ReadLine()!;
            Console.Write("Car model:");
            var model = Console.ReadLine()!;
            Console.Write("Fabrication year:");
            var year = int.Parse(Console.ReadLine()!);
            Console.Write("Category:");
            var categoryName = Console.ReadLine()!;
            Console.Write("Price:");
            var sellingPrice = decimal.Parse(Console.ReadLine()!);

            var result = _productService.CreateProduct(make, model, year, categoryName, sellingPrice);
            if (result != null)
            {
                Console.Clear();
                Console.WriteLine("New car succsefully added to the database.");
                Console.ReadKey();

            }
        }

        public void GetAllProducts_Menu()
        {
            Console.Clear();

            var products = _productService.GetProducts();
            if (products != null)
            {
                foreach (var product in products)
                {


                    Console.WriteLine($"{product.Make} {product.Model} {product.Year}-{product.Category.CategoryName} ({product.Price.SellingPrice} SEK) ");


                }

            }
            Console.ReadKey();
        }

        public void GetProductById_Menu()
        {
            Console.Clear();
            Console.Write("Enter the ID of the product you want to view: ");
            var id = int.Parse(Console.ReadLine()!);

            var product = _productService.GetProductById(id);
            if (product != null)
            {
                Console.WriteLine($"{product.Make} {product.Model} {product.Year}-{product.Category.CategoryName} ({product.Price.SellingPrice} SEK) ");
            }
            else
            {
                Console.WriteLine("No product found with the given ID.");
            }
            Console.ReadKey();
        }


        public void UpdateProduct_Menu()
        {
            Console.Clear();
            Console.Write("Enter the id of the car you would like to update:");
            var id = int.Parse(Console.ReadLine()!);

            var product = _productService.GetProductById(id);
            if (product != null)
            {
                Console.WriteLine($"{product.Make} {product.Model} {product.Year}-{product.Category.CategoryName} ({product.Price.SellingPrice} SEK) ");
                Console.WriteLine();


                Console.Write("New selling price:");
                decimal newPrice = decimal.Parse(Console.ReadLine()!);
                product.Price.SellingPrice = newPrice;

                var newProduct = _productService.UpdateProduct(product);
                Console.WriteLine($"{product.Make} {product.Model} {product.Year}-{product.Category.CategoryName} ({product.Price.SellingPrice} SEK) ");
            }
            else
            { Console.WriteLine("No car found!"); }
            Console.ReadKey();
        }

        public void DeleteProduct_Menu()
        {
            Console.Clear();
            Console.WriteLine("Enter the id of the car you would like to delete from the database:");
            var id = int.Parse(Console.ReadLine()!);
            var product = _productService.GetProductById(id);

            if (product != null)
            {
                _productService.DeleteProduct(id);
                Console.WriteLine("Succsesfuly deleted!");
            }
            else
            { Console.WriteLine("No car found!"); }
            Console.ReadKey();

        }

        public void CustomerMenu()
        {
            Console.Clear();
            Console.WriteLine("***** CUSTOMER SERVICES *****");
            Console.WriteLine("1. Add new customer");
            Console.WriteLine("2. Display customers list");
            Console.WriteLine("3. Display info about a specific customer(id)");
            Console.WriteLine("4. Display info about a specific customer(email)");
            Console.WriteLine("5. Update customer");
            Console.WriteLine("6. Delete customer");
            Console.WriteLine("7. Back to Main Menu");
            Console.Write("Please select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateCustomer_Menu();
                    break;
                case "2":
                    GetAllCustomers_Menu();
                    break;
                case "3":
                    GetOneCustomersbyId_Menu();
                    break;
                case "4":
                    GetCustomerByEmail_Menu();
                    break;
                case "5":
                    UpdateCustomer_Menu();
                    break;
                case "6":
                    DeleteCustomer_Menu();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        public void CreateCustomer_Menu()
        {
            Console.Clear();
            Console.WriteLine("*****ADD NEW CUSTOMER *****");
            Console.Write("Firstname:");
            var firstName = Console.ReadLine()!;
            Console.Write("Lastname:");
            var lastName = Console.ReadLine()!;
            Console.Write("Email address:");
            var email = Console.ReadLine()!;
            Console.Write("Phone number:");
            var phoneNumber = Console.ReadLine()!;
            Console.Write("Street name:");
            var streetName = Console.ReadLine()!;
            Console.Write("Postal code:");
            var postalCode = Console.ReadLine()!;
            Console.Write("City:");
            var city = Console.ReadLine()!;
            Console.Write("Car make:");
            var make = Console.ReadLine()!;
            Console.WriteLine("Car model:");
            var model = Console.ReadLine()!;
            Console.Write("Fabrication year:");
            var year = int.Parse(Console.ReadLine()!);
            Console.Write("Category:");
            var categoryName = Console.ReadLine()!;
            Console.Write("Price:");
            var sellingPrice = decimal.Parse(Console.ReadLine()!);

            var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNumber, streetName, postalCode, city, make, model, year, categoryName, sellingPrice);
            if (result != null)
            {
                Console.Clear();
                Console.WriteLine("New customer succsefully added to the database.");
                Console.ReadKey();

            }


        }

        public void GetAllCustomers_Menu()
        {
            Console.Clear();

            var customers = _customerService.GetCustomers();
            if (customers != null)
            {
                foreach (var customer in customers)
                {


                    Console.WriteLine($"Customer {customer.FirstName} {customer.LastName} {customer.Email} {customer.PhoneNumber} has the address: {customer.Address.StreetName} {customer.Address.PostalCode} {customer.Address.City} and bought this car: {customer.Product.Make} {customer.Product.Model} {customer.Product.Year} {customer.Product.Category.CategoryName} ({customer.Product.Price.SellingPrice} SEK) ");


                }
                Console.ReadKey();

            }
        }

        public void GetOneCustomersbyId_Menu()
        {
            Console.Clear();
            Console.Write("Enter the id of the customer you would like to display:");
            var id = int.Parse(Console.ReadLine()!);

            var customer = _customerService.GetCustomerById(id);
            if (customer != null)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName} {customer.Email} {customer.PhoneNumber} {customer.Address.StreetName} {customer.Address.PostalCode} {customer.Address.City} {customer.Product.Make} {customer.Product.Model} {customer.Product.Year} {customer.Product.Category.CategoryName} ({customer.Product.Price.SellingPrice} SEK)");
            }
            else
            {
                Console.WriteLine("No customer found!");
            }
            Console.ReadKey();
        }

        public void GetCustomerByEmail_Menu()
        {
            Console.Clear();
            Console.Write("Enter the email of the customer you would like to display:");
            var email = Console.ReadLine();

            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Email cannot be empty.");
                return;
            }

            var customer = _customerService.GetCustomerByEmail(email);
            if (customer != null)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName} {customer.Email} {customer.PhoneNumber} {customer.Address.StreetName} {customer.Address.PostalCode} {customer.Address.City} {customer.Product.Make} {customer.Product.Model} {customer.Product.Year} {customer.Product.Category.CategoryName} ({customer.Product.Price.SellingPrice} SEK)");
            }
            else
            {
                Console.WriteLine("No customer found!");
            }
            Console.ReadKey();
        }

        public void UpdateCustomer_Menu()
        {
            Console.Clear();
            Console.Write("Enter the id of the customer you would like to update:");
            var id = int.Parse(Console.ReadLine()!);

            var customer = _customerService.GetCustomerById(id);
            if (customer != null)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName} {customer.Email} {customer.PhoneNumber} {customer.Address.StreetName} {customer.Address.PostalCode} {customer.Address.City} {customer.Product.Make} {customer.Product.Model} {customer.Product.Year} {customer.Product.Category.CategoryName} ({customer.Product.Price.SellingPrice} SEK) ");
                Console.WriteLine();

                Console.Write("New email address:");
                var newEmail = Console.ReadLine()!;
                customer.Email = newEmail;

                var updatedCustomer = _customerService.UpdateCustomer(customer);
                Console.WriteLine($"Customer {updatedCustomer.FirstName} {updatedCustomer.LastName} {updatedCustomer.Email} {updatedCustomer.PhoneNumber} {updatedCustomer.Address.StreetName} {customer.Address.PostalCode} {updatedCustomer.Address.City} {updatedCustomer.Product.Make} {updatedCustomer.Product.Model} {updatedCustomer.Product.Year} {updatedCustomer.Product.Category.CategoryName} ({updatedCustomer.Product.Price.SellingPrice} SEK) ");

            }
            else
            {
                Console.WriteLine("No customer found!");
            }
            Console.ReadKey();
        }

        public void DeleteCustomer_Menu()
        {
            Console.Clear();
            Console.Write("Enter the id of the customer you would like to delete from the database:");
            var id = int.Parse(Console.ReadLine()!);
            var customer = _customerService.GetCustomerById(id);

            if (customer != null)
            {
                _customerService.DeleteCustomer(id);
                Console.WriteLine("Successfully deleted!");
            }
            else
            {
                Console.WriteLine("No customer found!");
            }
            Console.ReadKey();
        }


        public void ProductCatalogMenu()
        {
            Console.Clear();
            Console.WriteLine("***** PRODUCT CATALOG MENU *****");
            Console.WriteLine("1. Add new product");
            Console.WriteLine("2. Display product list");
            Console.WriteLine("3. Display one product ");
            Console.WriteLine("4. Update product");
            Console.WriteLine("5. Delete product");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Please select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateTires_Menu();
                    break;
                case "2":
                    GetAllTires_Menu();
                    break;
                case "3":
                    GetTirebyId_Menu();
                    break;
                case "4":
                    UpdateTire_Menu();
                    break;
                case "5":
                    DeleteTire_Menu();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        public void CreateTires_Menu()
        {
            Console.Clear();
            Console.WriteLine("*****ADD NEW PRODUCT *****");
            Console.Write("Brand:");
            var brand = Console.ReadLine()!;
            Console.Write("Size:");
            var size = Console.ReadLine()!;
            Console.Write("Type:");
            var type = Console.ReadLine()!;
            Console.Write("Seasonality:");
            var seasonality = Console.ReadLine()!;
            Console.Write("Quantity:");

            var quantity = int.Parse(Console.ReadLine()!);
            Console.Write("Price:");
            var price = decimal.Parse(Console.ReadLine()!);

            var result = _tiresService.CreateTire(brand, size, type, seasonality, price, quantity);
            if (result != null)
            {
                Console.Clear();
                Console.WriteLine("New product succsefully added to the database.");

            }
            Console.ReadKey();


        }

        public void GetAllTires_Menu()
        {
            Console.Clear();

            var tires = _tiresService.GetTires();
            if (tires != null)
            {
                foreach (var tire in tires)
                {


                    Console.WriteLine($"The product {tire.Brand} {tire.Size} {tire.Type} {tire.Seasonality} with the cost of ({tire.Price.Price1} SEK/st ) it is on the inventory with the quantity of:{tire.TireInventory.Quantity} pieces. ");


                }
                Console.ReadKey();

            }
        }
        public void GetTirebyId_Menu()
        {
            Console.Clear();
            Console.Write("Enter the id of the tire you would like to display:");
            var id = int.Parse(Console.ReadLine()!);

            var tire = _tiresService.GetTireById(id);
            if (tire != null)
            {
                Console.WriteLine($"{tire.Brand} {tire.Size} {tire.Type} {tire.Seasonality} ({tire.Price.Price1} SEK/st) {tire.TireInventory.Quantity}");
            }
            else
            {
                Console.WriteLine("No product found!");
            }
            Console.ReadKey();
        }


        public void UpdateTire_Menu()
        {
            Console.Clear();
            Console.Write("Enter the id of the tire you would like to update:");
            var id = int.Parse(Console.ReadLine()!);

            var tires = _tiresService.GetTireById(id);
            if (tires != null)
            {
                Console.WriteLine($"{tires.Brand} {tires.Size} {tires.Type} {tires.Seasonality} ({tires.Price.Price1} SEK/st) {tires.TireInventory.Quantity}  ");
                Console.WriteLine();

                Console.Write("New selling price:");
                decimal newPrice = decimal.Parse(Console.ReadLine()!);
                tires.Price.Price1 = newPrice;

                var updatedTires = _tiresService.UpdateTires(tires);
                Console.WriteLine($"Updated product {tires.Brand}{tires.Size} {tires.Type}{tires.Seasonality} ({tires.Price.Price1} SEK / st) {tires.TireInventory.Quantity}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No product found!");
            }
            Console.ReadKey();
        }

        public void DeleteTire_Menu()
        {
            Console.Clear();
            Console.Write("Enter the id of the product you would like to delete from the database:");
            var id = int.Parse(Console.ReadLine()!);
            var tires = _tiresService.GetTireById(id);

            if (tires != null)
            {
                _tiresService.DeleteTires(id);
                Console.WriteLine("Successfully deleted!");
            }
            else
            {
                Console.WriteLine("Noproduct found!");
            }
            Console.ReadKey();
        }

        public void TireServices_Menu()
        {
            Console.Clear();
            Console.WriteLine("***** TIRE SERVICE MENU *****");
            Console.WriteLine("1. Add new service");
            Console.WriteLine("2. Display service list");
            Console.WriteLine("3. Display one service ");
            Console.WriteLine("4. Update service");
            Console.WriteLine("5. Delete service");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Please select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateTireService_Menu();
                    break;
                case "2":
                    GetTireServices_Menu();
                    break;
                case "3":
                    GetTireServicebyId_Menu();
                    break;
                case "4":
                    UpdateTireService_Menu();
                    break;
                case "5":
                    DeleteTireService_Menu();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        public void CreateTireService_Menu()
        {
            Console.Clear();
            Console.Write("Enter the name of the service: ");
            var serviceName = Console.ReadLine()!;
            Console.Write("Enter the cost of the service: ");
            var cost = decimal.Parse(Console.ReadLine()!);

            var result = _tireservicesService.CreateTireService(serviceName, cost);
            if (result != null)
            {
                Console.WriteLine("New service successfully added to the database.");
            }
            else
            {
                Console.WriteLine("Failed to add new service.");
            }
            Console.ReadKey();
        }

        public void GetTireServices_Menu()
        {
            Console.Clear();
            var services = _tireservicesService.GetTireServices();
            foreach (var service in services)
            {
                Console.WriteLine($"Service Name: {service.ServiceName}, Cost: {service.Cost.Cost}");
            }
            Console.ReadKey();
        }

        public void GetTireServicebyId_Menu()
        {
            Console.Clear();
            Console.Write("Enter the ID of the service: ");
            var id = int.Parse(Console.ReadLine()!);
            var service = _tireservicesService.GetTireServiceById(id);
            if (service != null)
            {
                Console.WriteLine($"Service Name: {service.ServiceName}, Cost: {service.Cost.Cost}");
            }
            else
            {
                Console.WriteLine("No service found with the given ID.");
            }
            Console.ReadKey();
        }

        public void UpdateTireService_Menu()
        {
            Console.Clear();
            Console.Write("Enter the ID of the service to update: ");
            var id = int.Parse(Console.ReadLine()!);
            var service = _tireservicesService.GetTireServiceById(id);
            if (service != null)
            {
                Console.Write("Enter the new name of the service: ");
                service.ServiceName = Console.ReadLine()!;
                Console.Write("Enter the new cost of the service: ");
                var cost = decimal.Parse(Console.ReadLine()!);
                var servicePrice = new ServicePrice { Cost = cost }; 
                service.Cost = servicePrice; 
                var result = _tireservicesService.UpdateTireServices(service);
                if (result != null)
                {
                    Console.WriteLine("Service successfully updated.");
                }
                else
                {
                    Console.WriteLine("Failed to update service.");
                }
            }
            else
            {
                Console.WriteLine("No service found with the given ID.");
            }
            Console.ReadKey();
        }

        public void DeleteTireService_Menu()
        {
            Console.Clear();
            Console.Write("Enter the ID of the service to delete: ");
            var id = int.Parse(Console.ReadLine()!);
            _tireservicesService.DeleteTireService(id);
            Console.WriteLine("Service successfully deleted.");
            Console.ReadKey();
        }


    }


}







