using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessTools
{
    public static class PlaceTool
    {
        public static bool EstLibre(PlaceParking place, AddResBO reservation)
        {
            if(reservation.RES_DateFin < place.Reservation.First().RES_DateDebut || reservation.RES_DateDebut > place.Reservation.First().RES_DateFin)
            {
                return false;
                // Si la date de fin de la nouvelle réservation est antérieure à la date de début de la réservation existante
                //    // ou si la date de début de la nouvelle réservation est postérieure à la date de fin de la réservation existante,
                //    // alors elles ne se chevauchent pas
            }
            return true;// Si aucune des conditions ci-dessus n'est vraie, cela signifie que les réservations se chevauchent
        }

        
    }
}
