using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Reservations
{
    public int ResId { get; set; }

    public DateTime ResDateReservation { get; set; }

    public DateTime ResDateDebut { get; set; }

    public DateTime ResDateFin { get; set; }

    public decimal ResDureeTotalInitiale { get; set; }

    public decimal ResDureeTotalReele { get; set; }

    public bool ResCloture { get; set; }

    public int ResFormType { get; set; }

    public int PlaceId { get; set; }

    public int ClientId { get; set; }

    public int FactureId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Factures Facture { get; set; } = null!;

    public virtual Places? Places { get; set; }

    public virtual FormulesType ResFormTypeNavigation { get; set; } = null!;
}
