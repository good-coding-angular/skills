using System;
using System.Collections.Generic;

namespace skills_be.Models.Dto;

public partial class SkillDto
{
    public string SkillId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    
    public virtual IList<SkillgruppeDto>? Skillgruppen { get; set; }
    
    public virtual IList<MitarbeiterDto> Mitarbeiter { get; set; } = new List<MitarbeiterDto>();
}
