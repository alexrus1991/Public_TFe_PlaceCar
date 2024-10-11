using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Client
{
    public int CliId { get; set; }

    public virtual Personne Cli { get; set; } = null!;

    public virtual Comptes? Comptes { get; set; }

    public virtual ICollection<Preferences> Preferences { get; set; } = new List<Preferences>();

    public virtual ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
}
