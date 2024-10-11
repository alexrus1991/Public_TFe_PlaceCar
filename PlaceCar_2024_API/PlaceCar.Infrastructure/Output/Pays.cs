using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Pays
{
    public int PaysId { get; set; }

    public string PaysNom { get; set; } = null!;

    public virtual ICollection<Adresses> Adresses { get; set; } = new List<Adresses>();
}
