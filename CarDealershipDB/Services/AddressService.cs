
using CarDealershipDB.Entities;
using CarDealershipDB.Repositories;
using System.Diagnostics;

namespace CarDealershipDB.Services;

public class AddressService(AddressRepository addressRepository)
{
    private readonly AddressRepository _addressRepository = addressRepository;

    public AddressEntity CreateAddress(string streetName, string postalCode, string city)
    {
        try
        {
            var addressEntity = _addressRepository.Get(x => x.StreetName == streetName && x.PostalCode == postalCode && x.City == city);

            addressEntity ??= _addressRepository.Create(new AddressEntity { StreetName = streetName, PostalCode = postalCode, City = city });

            return addressEntity;
        }
        catch(Exception ex)
         { Debug.WriteLine(ex.Message); } 
        return null!;
    }

    public AddressEntity GetAddres(string streetName, string postalCode, string city)
    {
        try
        {
            var addressEntity = _addressRepository.Get(x => x.StreetName == streetName && x.PostalCode == postalCode && x.City == city);
            return addressEntity;
        }
        catch (Exception ex)
         { Debug.WriteLine(ex.Message); } 
        return null!;
    }

    public AddressEntity GetAddresById(int id)
    {
        try
        {
            var addressEntity = _addressRepository.Get(x => x.Id == id);
            return addressEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public IEnumerable<AddressEntity> GetAddresses()
    {
        try
        {
            var addresses = _addressRepository.GetAll();
            return addresses;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public AddressEntity UpdateAddres(AddressEntity addressEntity)
    {
        try
        {
            var updatedAddressEntity = _addressRepository.Update(x => x.Id == addressEntity.Id, addressEntity);
            return updatedAddressEntity;
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public void DeleteAddres(int id)
    {
        try
        {
            _addressRepository.Delete(x => x.Id == id);
        }
        catch (Exception ex)
        { Debug.WriteLine(ex.Message); }
      

    }
}


