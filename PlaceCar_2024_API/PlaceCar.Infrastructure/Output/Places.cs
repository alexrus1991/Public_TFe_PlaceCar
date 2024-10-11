using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Places
{
    public int PlaId { get; set; }

    public int PlaEtage { get; set; }

    public int PlaNumeroPlace { get; set; }

    public bool PlaEstLibre { get; set; }

    public int ParkingId { get; set; }

    public int ReservationId { get; set; }

    public virtual Parkings Parking { get; set; } = null!;

    public virtual ICollection<Preferences> Preferences { get; set; } = new List<Preferences>();

    public virtual Reservations Reservation { get; set; } = null!;
}
