using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.Wasm;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class ParkingEntity
    {
        public int PARK_Id { get; set; }
        public string PARK_Nom { get; set; } = string.Empty;
        public int PARK_NbEtages { get; set; } = 0;
        public int PARK_NbPlaces { get; set; } = 0;
        public int AdreseId { get; set; }
        public Adresse? Adresse { get; set; }
        public List<FormuleDePrix> Formules { get; set; } = [];

        public List<PlaceParking> Places { get; set; } = [];

        public List<Preferences> Preferences { get; set; } = [];

        public List<EmployeWorkOn> EmployeWorkOn { get; set; } = [];
       
    }
}
