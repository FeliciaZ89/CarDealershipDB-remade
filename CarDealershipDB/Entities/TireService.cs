using System;
using System.Collections.Generic;

namespace CarDealershipDB.Entities;

public partial class TireService
{
    public int Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public int CostId { get; set; }

    public virtual ServicePrice Cost { get; set; } = null!;
}
