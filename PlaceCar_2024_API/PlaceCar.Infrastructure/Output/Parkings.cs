using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Parkings
{
    public int ParkId { get; set; }

    public string ParkNom { get; set; } = null!;

    public int ParkNbEtages { get; set; }

    public int ParkNbPlaces { get; set; }

    public int AdreseId { get; set; }

    public virtual Adresses? Adresses { get; set; }

    public virtual ICollection<Formules> Formules { get; set; } = new List<Formules>();

    public virtual ICollection<Places> Places { get; set; } = new List<Places>();

    public virtual ICollection<Preferences> Preferences { get; set; } = new List<Preferences>();

    public virtual ICollection<Employee> EmpUtil { get; set; } = new List<Employee>();
}
