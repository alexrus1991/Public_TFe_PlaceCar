using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class Client
    {
        public int Cli_Id { get; set; }
        public virtual Personne Cli { get; set; } = null!;

        public virtual CompteBank? Comptes { get; set; }

        public List<Preferences> Preferences { get; set; } = [];

        public List<Reservation> Reservations { get; set; } = [];
    }
}
