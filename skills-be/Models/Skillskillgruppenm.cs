namespace skills_be.Models;

public partial class Skillskillgruppenm
{
    public int SkillSkillgruppeId { get; set; }

    public int SkillId { get; set; }

    public int SkillgruppeId { get; set; }

    public virtual Skill Skill { get; set; } = null!;

    public virtual Skillgruppe Skillgruppe { get; set; } = null!;
}
