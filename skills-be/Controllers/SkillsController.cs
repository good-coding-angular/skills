using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skills_be.Mapper;
using skills_be.Models;
using skills_be.Models.Dto;

namespace skills_be.Controllers;

[ApiController]
[Route(Endpoints.ApiPrefix + "[controller]")]
public class SkillsController : Controller
{
    public IConfiguration Configuration { get; }

    public SkillsController(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    [HttpGet("getSkills")]
    public IEnumerable<SkillDto> GetSkills()
    {
        using (var db = new SkillsContext(Configuration))
        {
            var skills = db.Skills.Include(s => s.Skillgruppen);
                
            var skillDtos = new SkillMapper()
                .MapSkillToSkillDto(skills)
                .ToArray();

            return skillDtos;
        }
    }
}