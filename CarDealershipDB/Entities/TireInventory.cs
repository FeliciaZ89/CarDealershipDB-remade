using System;
using System.Collections.Generic;

namespace CarDealershipDB.Entities;

public partial class TireInventory
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public virtual ICollection<Tire> Tires { get; set; } = new List<Tire>();
}
