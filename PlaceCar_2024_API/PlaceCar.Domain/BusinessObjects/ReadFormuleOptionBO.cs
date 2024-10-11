namespace PlaceCar.Domain.BusinessObjects
{
    public class ReadFormuleOptionBO
    {
        public int FormuleId { get; set; } // ID de la formule
        public string Description { get; set; } = string.Empty;
        public string PriceDetails { get; set; } = string.Empty;
        public decimal Total { get; set; }

        //Pour le calcul final de la facture
        public double totalAnnees { get; set; }
        public double totalDemiAnnee { get; set; }
        public double totalTrimestre { get; set; }
        public double totalMois { get; set; }
        public double totalSemaines { get; set; }
        public double totalJours { get; set; }
        public double totalHeurs { get; set; }

        public double PrixAnnee { get; set; }
        public double PrixDemiAnnee { get; set; }
        public double PrixTrimestre { get; set; }
        public double PrixMois { get; set; }
        public double PrixSemaine { get; set; }
        public double PrixlJour { get; set; }
        public double PrixHeur { get; set; }
    }

    public class DetailsPrix
    {
        //Pour le calcul final de la facture
        public double totalAnnees { get; set; }
        public double totalDemiAnnee { get; set; }
        public double totalTrimestre { get; set; }
        public double totalMois { get; set; }
        public double totalSemaines { get; set; }
        public double totalJours { get; set; }
        public double totalHeurs { get; set; }
    }
}
