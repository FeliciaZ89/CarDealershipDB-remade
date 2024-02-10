
using Microsoft.EntityFrameworkCore;
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using System.Diagnostics;
namespace CarDealershipDB.Services;

public class CustomerService(CustomerRepository customerRepository, AddressService addressService, ProductService productService)
{
    private readonly CustomerRepository _customerRepository = customerRepository;
    private readonly AddressService _addressService = addressService;
    private readonly ProductService _productService = productService;

    public CustomerEntity CreateCustomer(string firstName, string lastName, string email, string phoneNumber, string streetName, string postalCode, string city, string make, string model, int year, string categoryName, decimal sellingPrice)
    {
        try
        {
            var existingCustomer = _customerRepository.Get(c => c.Email == email);

            if (existingCustomer != null)
            {
               
                Debug.WriteLine("Customer with the same email already exists.");
                return null!;
            }

            var addressEntity = _addressService.CreateAddress(streetName, postalCode, city);
            var productEntity = _productService.CreateProduct(make, model, year, categoryName, sellingPrice);

       
            var customerEntity = new CustomerEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                AddressId = addressEntity.Id,
                ProductId = productEntity.Id
            };

            customerEntity = _customerRepository.Create(customerEntity);
            return customerEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);

            throw;
           
        }
       
    }





    public CustomerEntity GetCustomerById(int id)
    {
        try
        {
            var customerEntity = _customerRepository.Get(x => x.Id == id);
            return customerEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;

    }
    public CustomerEntity GetCustomerByEmail(string email)
    {
        try
        {
            var customerEntity = _customerRepository.Get(x => x.Email == email);
            return customerEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }
    public IEnumerable<CustomerEntity> GetCustomers()
    {
        try
        {
            var customers = _customerRepository.GetAll()
                                                .Include(p => p.Address)
                                                .Include(p => p.Product)
                                                .ThenInclude(p => p.Category)
                                                .Include(p => p.Product)
                                                .ThenInclude(p => p.Price)
                                                .ToList();

            return customers;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public CustomerEntity UpdateCustomer(CustomerEntity customerEntity)
    {
        try
        {
            var updatedCustomerEntity = _customerRepository.Update(x => x.Id == customerEntity.Id, customerEntity);
            return updatedCustomerEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public void DeleteCustomer(int id)
    {
        try
        {
            _customerRepository.Delete(x => x.Id == id);
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }


    }
}
