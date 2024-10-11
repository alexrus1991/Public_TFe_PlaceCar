using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class FormulesType
{
    public int FormTypeId { get; set; }

    public string FormTitle { get; set; } = null!;

    public string FormTypeDescription { get; set; } = null!;

    public int FormuleId { get; set; }

    public virtual Formules Formule { get; set; } = null!;

    public virtual ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
}
