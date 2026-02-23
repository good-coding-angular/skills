namespace skills_be.Models;

public partial class Skillgruppe
{
    public int SkillGruppeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

    public virtual ICollection<Skillskillgruppenm> Skillskillgruppenms { get; set; } = new List<Skillskillgruppenm>();
}
