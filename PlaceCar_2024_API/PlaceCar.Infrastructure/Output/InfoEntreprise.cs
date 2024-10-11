using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class InfoEntreprise
{
    public string Nom { get; set; } = null!;

    public string CbNumCompte { get; set; } = null!;

    public virtual ICollection<Transactions> Transactions { get; set; } = new List<Transactions>();
}
