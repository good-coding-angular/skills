namespace skills_be.Models;

public partial class Skill
{
    public int SkillId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Mitarbeiter> Mitarbeiter { get; set; } = new List<Mitarbeiter>();

    public virtual ICollection<Mitarbeiterskillnm> Mitarbeiterskillnms { get; set; } = new List<Mitarbeiterskillnm>();

    public virtual ICollection<Skillgruppe> Skillgruppen { get; set; } = new List<Skillgruppe>();

    public virtual ICollection<Skillskillgruppenm> Skillskillgruppenms { get; set; } = new List<Skillskillgruppenm>();
}
