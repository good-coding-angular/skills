using System;
using System.Collections.Generic;

namespace skills_be.Models;

public partial class Skilllearningsource
{
    public string SkilllearningsourceId { get; set; } = null!;

    public string? SourceName { get; set; }

    public string? Url { get; set; }
}
