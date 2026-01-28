using System;
using System.Collections.Generic;

namespace skills_be.Models;

public partial class Skillgruppe
{
    public string SkillGruppeId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

    public virtual ICollection<Skillskillgruppenm> Skillskillgruppenms { get; set; } = new List<Skillskillgruppenm>();
}
