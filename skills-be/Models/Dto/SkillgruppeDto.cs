using System;
using System.Collections.Generic;

namespace skills_be.Models.Dto;

public partial class SkillgruppeDto
{
    public int SkillGruppeId { get; set; }

    public string Name { get; set; } = null!;
    
    public virtual ICollection<SkillDto>? Skills { get; set; }
}
