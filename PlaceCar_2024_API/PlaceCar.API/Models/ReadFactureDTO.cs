namespace PlaceCar.API.Models
{
    public class ReadFactureDTO
    {
        public int FACT_Id { get; set; }
        public decimal FACT_Somme { get; set; }
        public DateTime FACT_Date { get; set; }
        public int RES_Id { get; set; }
        public DateTime RES_DateReservation { get; set; }
        public DateTime RES_DateDebut { get; set; }
        public DateTime? RES_DateFin { get; set; }
    }
}
