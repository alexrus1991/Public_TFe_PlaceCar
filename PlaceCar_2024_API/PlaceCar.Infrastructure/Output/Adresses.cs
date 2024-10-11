using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Adresses
{
    public int AdrsId { get; set; }

    public int AdrsNumero { get; set; }

    public string AdrsNomRue { get; set; } = null!;

    public string AdrsVille { get; set; } = null!;

    public decimal AdrsLatitude { get; set; }

    public decimal AdrsLongitude { get; set; }

    public int ParkingId { get; set; }

    public int PaysId { get; set; }

    public virtual Parkings Parking { get; set; } = null!;

    public virtual Pays Pays { get; set; } = null!;
}
