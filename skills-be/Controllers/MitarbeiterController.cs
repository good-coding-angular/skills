using Microsoft.AspNetCore.Mvc;
using skills_be.Models;
using skills_be.Models.Dto;

namespace skills_be.Controllers;

[ApiController]
[Route(Endpoints.ApiPrefix + "[controller]")]
public class MitarbeiterController
{
    public IConfiguration Configuration { get; }

    public MitarbeiterController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [HttpGet("getAllMitarbeiter")]
    public IEnumerable<MitarbeiterDto> GetAllMitarbeiter()
    {
        using (var db = new SkillsContext(Configuration))
        {
            var mitarbeiter = db.Mitarbeiters
                .Select(m => new MitarbeiterDto
                {
                    MitarbeiterId = m.MitarbeiterId,
                    Name = m.Name,
                    Available = m.Available
                })
                .ToList();

            return mitarbeiter;
        }
    }
}

