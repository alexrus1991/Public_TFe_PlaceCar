using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Factures
{
    public int FactId { get; set; }

    public decimal FactSomme { get; set; }

    public DateTime FactDate { get; set; }

    public int ReservationId { get; set; }

    public int TransactionId { get; set; }

    public virtual Reservations? Reservations { get; set; }

    public virtual Transactions? Transactions { get; set; }
}
