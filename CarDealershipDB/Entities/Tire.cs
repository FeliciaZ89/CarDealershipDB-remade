using System;
using System.Collections.Generic;

namespace CarDealershipDB.Entities;

public partial class Tire
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Size { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Seasonality { get; set; } = null!;

    public int PriceId { get; set; }

    public int TireInventoryId { get; set; }

    public virtual Price Price { get; set; } = null!;

    public virtual TireInventory TireInventory { get; set; } = null!;
}
