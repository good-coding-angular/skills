using System;
using System.Collections.Generic;

namespace skills_be.Models.Dto;

public partial class SkilllearningsourceDto
{
    public string SkilllearningsourceId { get; set; } = null!;

    public string? SourceName { get; set; }

    public string? Url { get; set; }
}
