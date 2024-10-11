using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class ReadFactureStripeBO
    {
        public int FACT_Id { get; set; }
        public string? StripeConfirmStr { get; set; }
    }
}
