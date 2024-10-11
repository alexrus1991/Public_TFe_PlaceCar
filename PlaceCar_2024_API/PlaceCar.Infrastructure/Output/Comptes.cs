using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Comptes
{
    public int CbId { get; set; }

    public string CbNom { get; set; } = null!;

    public string CbNumCompte { get; set; } = null!;

    public DateTime CbDate { get; set; }

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Transactions> Transactions { get; set; } = new List<Transactions>();
}
