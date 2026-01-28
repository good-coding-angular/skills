using System;
using System.Collections.Generic;

namespace skills_be.Models.Dto;

public partial class MitarbeiterskillnmDto
{
    public string MitarbetierSkillId { get; set; } = null!;

    public string SkillId { get; set; } = null!;

    public string MitarbeiterId { get; set; } = null!;

    public int Level { get; set; }
    
    public virtual Skill Skill { get; set; } = null!;

    public virtual Mitarbeiter Mitarbeiter { get; set; } = null!;
}
