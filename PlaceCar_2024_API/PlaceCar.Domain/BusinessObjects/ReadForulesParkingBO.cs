using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.BusinessObjects
{
    public class ReadForulesParkingBO
    {
        public int FORM_Id { get; set; }
        public decimal FORM_Prix { get; set; }
        public string FORM_Title { get; set; }
        public string FORM_Type_Description { get; set; } 
    }
}
