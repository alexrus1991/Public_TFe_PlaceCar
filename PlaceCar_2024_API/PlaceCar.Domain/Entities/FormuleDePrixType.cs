using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class FormuleDePrixType
    {
        public int FORM_Type_Id { get; set; }
        public string FORM_Title { get; set; } = string.Empty;
        public string FORM_Type_Description { get; set; } = string.Empty;
        public decimal FORM_Type_Duree { get; set; }   
      
        public virtual List<FormuleDePrix> FormuleDePrix { get; set; }
       
    }
}
