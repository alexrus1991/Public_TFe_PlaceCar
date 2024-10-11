using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Preferences
{
    public int PlaId { get; set; }

    public int ParkId { get; set; }

    public int CliId { get; set; }

    public virtual Client Cli { get; set; } = null!;

    public virtual Parkings Park { get; set; } = null!;

    public virtual Places Pla { get; set; } = null!;
}
