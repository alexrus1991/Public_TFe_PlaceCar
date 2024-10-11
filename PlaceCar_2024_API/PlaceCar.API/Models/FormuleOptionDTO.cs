namespace PlaceCar.API.Models
{
    public class FormuleOptionDTO
    {
        public int FormuleId { get; set; } // ID de la formule
        public string Description { get; set; } = string.Empty;
        public string PriceDetails { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }
}
