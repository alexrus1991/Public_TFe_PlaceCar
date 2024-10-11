using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Stripe
{
    public class CreateCheckoutSessionResponse
    {
        public string SessionId { get; set; }
        public string PublicKey { get; set; }
    }
}
