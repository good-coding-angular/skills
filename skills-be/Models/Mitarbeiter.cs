using System;
using System.Collections.Generic;

namespace skills_be.Models;

public partial class Mitarbeiter
{
    public string MitarbeiterId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Available { get; set; }

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

    public virtual ICollection<Mitarbeiterskillnm> Mitarbeiterskillnms { get; set; } = new List<Mitarbeiterskillnm>();
}
