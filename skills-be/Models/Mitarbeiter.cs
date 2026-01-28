using System;
using System.Collections.Generic;

namespace skills_be.Models;

public partial class Mitarbeiter
{
    public string MitarbeiterId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Available { get; set; }
}
