using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceCar.Domain.Entities
{
    public class Role
    {
        public int Role_Id { get; set; }
        public string Role_Name { get; set;}
        public List<RolePermission> PermissionRoles { get; set; } = [];
        public List<PersonneRole> PersonnesRoles { get; set; } = [];
    }
}
