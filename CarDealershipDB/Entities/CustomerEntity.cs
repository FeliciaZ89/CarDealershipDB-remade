
using System.ComponentModel.DataAnnotations;

namespace CarDealershipDB.Entities;

public class CustomerEntity
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }

    //Foreign keys
    public int AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!;
    public int ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;

}
