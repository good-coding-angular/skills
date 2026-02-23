namespace skills_be.Models;

public partial class Projekt
{
    public int ProjektId { get; set; }

    public string Projektname { get; set; } = null!;

    public string? Projektende { get; set; }
}
