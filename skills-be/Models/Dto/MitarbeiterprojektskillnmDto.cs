using System;
using System.Collections.Generic;

namespace skills_be.Models.Dto;

public partial class MitarbeiterprojektskillnmDto
{
    public string MitarbeiterProjektSkillNmid { get; set; } = null!;

    public string MitarbeiterId { get; set; } = null!;

    public string ProjektId { get; set; } = null!;

    public string? SkillId { get; set; }
}
