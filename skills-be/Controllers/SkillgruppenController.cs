using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skills_be.Mapper;
using skills_be.Models;
using skills_be.Models.Dto;

namespace skills_be.Controllers;

[ApiController]
[Route(Endpoints.ApiPrefix + "[controller]")]
public class SkillgruppenController
{
    public IConfiguration Configuration { get; }

    public SkillgruppenController(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [HttpGet("getSkillgruppen")]
    public IEnumerable<SkillgruppeDto> GetSkillgruppen()
    {
        using (var db = new SkillsContext(Configuration))
        {
            var skillgruppen = db.Skillgruppes
                .Include(g => g.Skills)
                .ToList();
            
            var result = new SkillgruppenMapper()
                .MapSkillgruppeToSkillgruppeDto(skillgruppen)
                .ToArray();

            return result;
        }
    }
}

