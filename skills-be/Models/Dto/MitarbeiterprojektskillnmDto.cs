using System;
using System.Collections.Generic;

namespace skills_be.Models.Dto;

public partial class MitarbeiterprojektskillnmDto
{
    public int MitarbeiterProjektSkillNmid { get; set; }

    public int MitarbeiterId { get; set; }

    public int ProjektId { get; set; }

    public int? SkillId { get; set; }
}
