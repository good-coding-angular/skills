using System;
using System.Collections.Generic;

namespace skills_be.Models.Dto;

public partial class MitarbeiterskillnmDto
{
    public int MitarbetierSkillId { get; set; }

    public int SkillId { get; set; }

    public int MitarbeiterId { get; set; }

    public int Level { get; set; }
    
    public virtual Skill Skill { get; set; } = null!;

    public virtual Mitarbeiter Mitarbeiter { get; set; } = null!;
}
