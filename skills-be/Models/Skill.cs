using System;
using System.Collections.Generic;

namespace skills_be.Models;

public partial class Skill
{
    public string SkillId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Skillgruppe> Skillgruppen { get; set; } = new List<Skillgruppe>();

    public virtual ICollection<Skillskillgruppenm> Skillskillgruppenms { get; set; } = new List<Skillskillgruppenm>();
}
