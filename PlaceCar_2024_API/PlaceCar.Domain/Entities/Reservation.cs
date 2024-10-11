using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class Reservation
    {
        public int RES_Id { get; set; }
        public DateTime RES_DateReservation { get; set; } 
        public DateTime RES_DateDebut { get; set; }
        public DateTime? RES_DateFin { get; set; } 
        public decimal RES_DureeTotal_Initiale { get; set; } 
        public decimal RES_DureeTotal_Reele { get; set; } 
        public bool RES_Cloture { get; set; }
        

        public int PlaceId { get; set; }
        public PlaceParking? PlaceParking { get; set; }

        public int ClientId { get; set; }
        public Client? Client { get; set;}

        public int? FactureId { get; set; }
        public Facture? Facture { get; set; }
        public int FormPrixId { get; set; }
        public FormuleDePrix ResFormPrixNavigation { get; set; } = null!;

    }
}
