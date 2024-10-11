using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Stripe
{
    public class CreateCheckoutSessionRequest
    {
        [Required]
        public long PriceId { get; set; }

        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string FactureId { get; set; }

        [Required]
        public string Communication { get; set;}

        [Required]
        public long CompteUnId { get; set; }

        [Required]
        public string SuccessUrl { get; set; }
        [Required]
        public string FailureUrl { get; set; }
    }
}
