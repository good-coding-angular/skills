using System;
using System.Collections.Generic;

namespace skills_be.Models.Dto;

public partial class MitarbeiterDto
{
    public string MitarbeiterId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Available { get; set; }
}
