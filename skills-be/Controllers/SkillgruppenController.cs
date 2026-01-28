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
            var skillgruppen = db.Skillgruppen
                .Include(g => g.Skills)
                .ToList();
            
            var result = new SkillgruppenMapper()
                .MapSkillgruppeToSkillgruppeDto(skillgruppen)
                .ToArray();

            return result;
        }
    }

    [HttpGet("getSkillgruppenWithUsers")]
    public IEnumerable<SkillgruppeDto> GetSkillgruppenWithUsers()
    {
        using (var db = new SkillsContext(Configuration))
        {
            var skillgruppen = db.Skillgruppen
                .Include(sg => sg.Skills)
                .ThenInclude(s => s.Mitarbeiter)
                .ToList();
            
            var result = new SkillgruppenMapper()
                .MapSkillgruppeToSkillgruppeDto(skillgruppen)
                .ToArray();

            return result;
        }
    }
}

