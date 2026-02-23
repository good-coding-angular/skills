namespace skills_be.Models;

public partial class Mitarbeiterprojektskillnm
{
    public int MitarbeiterProjektSkillNmid { get; set; }

    public int MitarbeiterId { get; set; }

    public int ProjektId { get; set; }

    public int? SkillId { get; set; }
}
