using Microsoft.AspNetCore.Mvc;
using skills_be.Mapper;
using skills_be.Models;
using skills_be.Models.Dto;

namespace skills_be.Controllers;

[ApiController]
[Route(Endpoints.ApiPrefix + "[controller]")]
public class MitarbeiterController : Controller
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

    [HttpPost("createMitarbeiter")]
    public MitarbeiterDto CreateMitarbeiter([FromBody] MitarbeiterDto dto)
    {
        using (var db = new SkillsContext(Configuration))
        {
            var entity = new Mitarbeiter
            {
                MitarbeiterId = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Available = dto.Available
            };

            db.Mitarbeiters.Add(entity);
            db.SaveChanges();

            return new MitarbeiterMapper().MapSingleMitarbeiterToMitarbeiterDto(entity);
        }
    }

    [HttpPost("updateMitarbeiter")]
    public ActionResult<MitarbeiterDto> UpdateMitarbeiter([FromBody] MitarbeiterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.MitarbeiterId))
        {
            return BadRequest("MitarbeiterId is required for update.");
        }

        using (var db = new SkillsContext(Configuration))
        {
            var entity = db.Mitarbeiters.FirstOrDefault(m => m.MitarbeiterId == dto.MitarbeiterId);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = dto.Name;
            entity.Available = dto.Available;

            db.SaveChanges();

            var result = new MitarbeiterDto
            {
                MitarbeiterId = entity.MitarbeiterId,
                Name = entity.Name,
                Available = entity.Available
            };

            return Ok(result);
        }
    }
}

