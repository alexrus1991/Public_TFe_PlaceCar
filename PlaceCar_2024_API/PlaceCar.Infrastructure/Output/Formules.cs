using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Formules
{
    public int FormId { get; set; }

    public decimal FormPrix { get; set; }

    public int TypeId { get; set; }

    public int ParkingId { get; set; }

    public virtual FormulesType? FormulesType { get; set; }

    public virtual Parkings Parking { get; set; } = null!;
}
