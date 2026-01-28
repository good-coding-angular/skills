using System;
using System.Collections.Generic;

namespace skills_be.Models;

public partial class Skillskillgruppenm
{
    public string SkillSkillgruppeId { get; set; } = null!;

    public string SkillId { get; set; } = null!;

    public string SkillgruppeId { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;

    public virtual Skillgruppe Skillgruppe { get; set; } = null!;
}
