using System;
using System.Collections.Generic;

namespace PlaceCar.Infrastructure.output;

public partial class Personne
{
    public int UtilId { get; set; }

    public string UtilNom { get; set; } = null!;

    public string UtilPrenom { get; set; } = null!;

    public DateTime UtilDateNaissance { get; set; }

    public string UtilEmail { get; set; } = null!;

    public string UtilPassword { get; set; } = null!;

    public string UtilTypeUtilisateur { get; set; } = null!;

    public int CompteId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Employee? Employee { get; set; }
}
