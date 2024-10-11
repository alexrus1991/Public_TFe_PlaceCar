namespace PlaceCar.API.Models
{
    public class ReadReservationParkingDTO
    {
        public int RES_Id { get; set; }
        public DateTime RES_DateDebut { get; set; }
        public DateTime RES_DateFin { get; set; }
        public int PLA_Id { get; set; }
        public string PERS_Nom { get; set; } = string.Empty;
        public string PERS_Prenom { get; set; } = string.Empty;
        public int PERS_Id { get; set; }
    }
}
