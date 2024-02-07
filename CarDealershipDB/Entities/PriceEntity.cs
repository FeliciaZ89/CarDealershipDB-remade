

using System.ComponentModel.DataAnnotations;

namespace CarDealershipDB.Entities;

public class PriceEntity
{
    [Key]
    public int Id { get; set; }

    public decimal SellingPrice { get; set; }
}
