namespace PlaceCar.API.Models
{
    public class PlaceStatusDto
    {
        public int PlaceId { get; set; }
        public int NumeroPlace { get; set; }
        //0 : Occupé : rouge
        //1: libre : verte
        //2: réservation se termine a la date demandé : couleur orange
        public int Status { get; set; }
    }
}
