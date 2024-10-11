using PlaceCar.Domain.BusinessObjects;
using PlaceCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessTools
{
    public static class ReservationTool
    {
        public static decimal CalculeTempsInitiale(DateTime dateDeb, DateTime dateFin)
        {
            TimeSpan t = dateFin - dateDeb;
            decimal tot = Math.Round((decimal)t.TotalHours);
            return tot;
        }

        public static decimal CalculeTempsReel(DateTime dateDeb, DateTime dateReel)
        {
            TimeSpan t = dateReel - dateDeb;
            decimal tot = Math.Round((decimal)t.TotalHours);
            return tot;
        }

        public static decimal CalculeDifferenceInit_Reel(decimal dureeInit, decimal dureeReel)
        {

            decimal i = Math.Round(dureeInit);
            decimal d = Math.Round(dureeReel);

            decimal reponce = (d) - (i);

            return reponce;
        }

        public static Reservation CalculeDateFin(Reservation res, FormuleDePrix formuleDePrix)// le cas ou df pas choisie mai forprixId est remplis
        {
            if (res.RES_DateFin == null && res.FormPrixId != 0)
            {
                res.RES_DateFin = Get_Date_Fin(res.RES_DateDebut, formuleDePrix.FormuleDePrixType.FORM_Title);
            }
           
            else if (res.RES_DateFin == null && res.FormPrixId == 0)
            {
                res.RES_DateFin = res.RES_DateFin;
            }
            else { res.RES_DateFin = res.RES_DateFin; }
            return res;
        }

        public static DateTime Get_Date_Fin(DateTime dd,string nomType)
        {
            DateTime df;
            switch (nomType)
            {
                case "Heure":
                    df = dd.AddHours(1);
                    break;

                case "Jour":
                    df = dd.AddDays(1);
                    break;

                case "Semaine":
                    df = dd.AddDays(7);
                    break;

                case "Monsuelle":
                    df = dd.AddMonths(1);
                    break;

                case "Trimestre":
                    df = dd.AddMonths(3);
                    break;

                case "Demi-Année":
                    df = dd.AddMonths(6);
                    break;

                case "Annee":
                    df = dd.AddYears(1);
                    break;

                default:
                    throw new ArgumentException("Le tipe de formule n'est pas correcte");
            }
            return df;
        }


      
    }
}
