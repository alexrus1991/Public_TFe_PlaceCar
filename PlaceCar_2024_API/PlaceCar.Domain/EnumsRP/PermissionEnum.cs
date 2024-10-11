using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.EnumsRP
{
    public enum PermissionEnum
    {
        read_admin = 1,
        
        write_admin = 2, 
        
        read_parking = 3,
        
        write_parking = 4,
        
        read_all = 5, 
        
        write_all = 6
    }
}
