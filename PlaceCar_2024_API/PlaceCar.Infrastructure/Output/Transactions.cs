using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Transactions
{
    public int TransId { get; set; }

    public decimal TransSomme { get; set; }

    public DateTime TransDate { get; set; }

    public string TransCommunication { get; set; } = null!;

    public int FactureId { get; set; }

    public int CompteUnId { get; set; }

    public string CompteEntreprise { get; set; } = null!;

    public virtual InfoEntreprise CompteEntrepriseNavigation { get; set; } = null!;

    public virtual Comptes CompteUn { get; set; } = null!;

    public virtual Factures Facture { get; set; } = null!;
}
