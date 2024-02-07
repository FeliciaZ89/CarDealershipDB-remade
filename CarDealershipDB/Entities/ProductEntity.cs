

using System.ComponentModel.DataAnnotations;

namespace CarDealershipDB.Entities;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }
    public string Make { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }

    //Foreign keys
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
    public int PriceId { get; set; }
    public PriceEntity Price { get; set; } = null!;

}
