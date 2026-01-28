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
                .ThenInclude(s => s.Mitarbeiterskillnms)
                .ThenInclude(s => s.Mitarbeiter)
                .ToList();
            
            var result = new SkillgruppenMapper()
                .MapSkillgruppeToSkillgruppeDto(skillgruppen)
                .ToArray();

            return result;
        }
    }

    [HttpPost("getFilteredSkillgruppen")]
    public IEnumerable<SkillgruppeDto> GetFilteredSkillgruppen([FromBody] GetFilteredSkillgruppenPostDto filter)
    {
        using (var db = new SkillsContext(Configuration))
        {
            var skillgruppen = db.Skillgruppen
                .Include(sg => sg.Skills)
                .ThenInclude(s => s.Mitarbeiterskillnms)
                .ThenInclude(ms => ms.Mitarbeiter)
                .ToList();

            var userIds = filter.Users?.ToHashSet() ?? new HashSet<string>();
            var skillIds = filter.Skills?.ToHashSet() ?? new HashSet<string>();

            foreach (var sg in skillgruppen)
            {
                if (skillIds.Count != 0)
                {
                    sg.Skills = sg.Skills
                        .Where(s => skillIds.Contains(s.SkillId))
                        .ToList();
                }

                foreach (var skill in sg.Skills)
                {
                    skill.Mitarbeiterskillnms = skill.Mitarbeiterskillnms
                        .Where(ms => userIds.Contains(ms.Mitarbeiter.MitarbeiterId))
                        .ToList();
                }
            }
            
            var result = new SkillgruppenMapper()
                .MapSkillgruppeToSkillgruppeDto(skillgruppen)
                .ToArray();

            return result;
        }
    }
}

