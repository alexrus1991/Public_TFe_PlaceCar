using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Models
{
    public class AddFormPrixDTO
    {
        public decimal Prix { get; set; }

        public int TypeId { get; set; }

        public int ParkingId { get; set; }
    }
}
