using System;
using System.Collections.Generic;

namespace skills_be.Models.Dto;

public partial class MitarbeiterskillnmDto
{
    public string MitarbetierSkillId { get; set; } = null!;

    public string SkillId { get; set; } = null!;

    public string MitarbeiterId { get; set; } = null!;

    public int Level { get; set; }
}
