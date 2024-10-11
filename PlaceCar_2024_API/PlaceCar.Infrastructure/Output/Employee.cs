using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Employee
{
    public int EmpUtilId { get; set; }

    public bool IsAdmin { get; set; }

    public virtual Personne EmpUtil { get; set; } = null!;

    public virtual ICollection<Parkings> Parking { get; set; } = new List<Parkings>();
}
