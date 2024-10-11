using PlaceCar.Domain.Entities;

namespace PlaceCar.API.Models
{
    public class ReadFactureStripeDto
    {
        public int FACT_Id { get; set; }
        public string? StripeConfirmStr { get; set; }
    }
}
