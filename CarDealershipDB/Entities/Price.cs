using System;
using System.Collections.Generic;

namespace CarDealershipDB.Entities;

public partial class Price
{
    public int Id { get; set; }

    public decimal Price1 { get; set; }

    public virtual ICollection<Tire> Tires { get; set; } = new List<Tire>();
}
