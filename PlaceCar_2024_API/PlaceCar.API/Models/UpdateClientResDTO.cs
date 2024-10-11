namespace PlaceCar.API.Models
{
    public class UpdateClientResDTO
    {
        public int RES_Id { get; set; }
        public int ClientId { get; set; }
        //public DateTime? RES_DateDebut { get; set; }
        public DateTime? RES_DateFin { get; set; }
        public int? PlaceId { get; set; }
        public int? FormPrixId { get; set; }
    }
}
