using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class Permission
    {
        public int Perm_id { get; set; }
        public string Perm_name { get; set; }
        public List<RolePermission> RolePermissions { get; set; } = [];
    }
}
