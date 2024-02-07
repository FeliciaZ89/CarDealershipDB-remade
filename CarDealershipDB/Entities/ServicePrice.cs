using System;
using System.Collections.Generic;

namespace CarDealershipDB.Entities;

public partial class ServicePrice
{
    public int Id { get; set; }

    public decimal Cost { get; set; }

    public virtual ICollection<TireService> TireServices { get; set; } = new List<TireService>();
}
