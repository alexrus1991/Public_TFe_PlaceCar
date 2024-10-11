using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class UpdateClientBO
    {
        public int Client_Id { get; set; }
        public string UTIL_Email { get; set; }
        public string UTIL_Password { get; set; }
    }
}
