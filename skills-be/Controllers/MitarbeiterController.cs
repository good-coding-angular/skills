using System.DirectoryServices.AccountManagement;
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
                Name = dto.Name,
                Available = dto.Available
            };

            db.Mitarbeiters.Add(entity);
            db.SaveChanges();

            return new MitarbeiterMapper().MapSingleMitarbeiterToMitarbeiterDto(entity);
        }
    }

    [HttpGet("getEmployeesFromActiveDirectory")]
    public ActionResult<IEnumerable<ActiveDirectoryUserDto>> GetEmployeesFromActiveDirectory()
    {
        var domain = Configuration["ActiveDirectory:Domain"];
        var searchBase = Configuration["ActiveDirectory:SearchBase"];
        var username = Configuration["ActiveDirectory:Username"];
        var password = Configuration["ActiveDirectory:Password"];

        try
        {
            using var context = string.IsNullOrWhiteSpace(username)
                ? new PrincipalContext(ContextType.Domain, domain)
                : new PrincipalContext(ContextType.Domain, domain, searchBase, username, password);

            using var searcher = new PrincipalSearcher(new UserPrincipal(context) { Enabled = true });

            var users = searcher.FindAll()
                .Cast<UserPrincipal>()
                .Select(u =>
                {
                    if (u.EmployeeId == null)
                    {
                        return null;
                    }

                    Console.WriteLine("u.EmployeeId: " + u.EmployeeId);
                    Console.WriteLine("u.Guid: " + u.Guid);
                    return new ActiveDirectoryUserDto
                    {
                        SamAccountName = u.EmployeeId,
                        DisplayName = u.DisplayName,
                        Email = u.EmailAddress,
                        UserPrincipalName = u.UserPrincipalName
                    };
                })
                .ToList();

            using (var db = new SkillsContext(Configuration))
            {
                foreach (var user in users)
                {
                    if (user != null)
                    {
                        var existing = db.Mitarbeiters
                            .FirstOrDefault(m => m.EmployeeId == user.SamAccountName);

                        if (existing == null)
                        {
                            if (user.DisplayName != null)
                            {
                                db.Mitarbeiters.Add(new Mitarbeiter
                                {
                                    EmployeeId = user.SamAccountName,
                                    Name = user.DisplayName,
                                    Available = 0
                                });
                            }
                        }
                        else
                        {
                            existing.Name = user.DisplayName;
                        }
                    }
                }

                db.SaveChanges();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null)
            {
                return StatusCode(500, $"Failed to connect to Active Directory: {ex.Message}, {ex.InnerException.Message}");
            }
            return StatusCode(500, $"Failed to connect to Active Directory: {ex.Message}");
        }
    }

    [HttpPost("updateMitarbeiter")]
    public ActionResult<MitarbeiterDto> UpdateMitarbeiter([FromBody] MitarbeiterDto dto)
    {
        if (dto.MitarbeiterId == null)
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

