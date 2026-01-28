using System;
using System.Collections.Generic;

namespace skills_be.Models;

public partial class Projekt
{
    public string ProjektId { get; set; } = null!;

    public string Projektname { get; set; } = null!;

    public string? Projektende { get; set; }
}
