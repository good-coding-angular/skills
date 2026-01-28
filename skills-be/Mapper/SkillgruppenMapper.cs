using skills_be.Models;
using skills_be.Models.Dto;

namespace skills_be.Mapper;

public class SkillgruppenMapper
{
    public IList<SkillgruppeDto> MapSkillgruppeToSkillgruppeDto(
        IEnumerable<Skillgruppe> skillgruppen
    ) {
        var skillgruppeDtos = new List<SkillgruppeDto>();

        foreach (var skillgruppe in skillgruppen)
        {
            var skillgruppeDto = new SkillgruppeDto
            {
                SkillGruppeId = skillgruppe.SkillGruppeId,
                Name = skillgruppe.Name,
                Skills = MapSkillToSkillDtos(skillgruppe.Skills)
            };

            skillgruppeDtos.Add(skillgruppeDto);
        }

        return skillgruppeDtos;
    }
    
    public IList<SkillDto> MapSkillToSkillDtos(
        IEnumerable<Skill> skills
    ) {
        var skillDtos = new List<SkillDto>();

        foreach (var skillgruppe in skills)
        {
            var skillDto = new SkillDto
            {
                SkillId = skillgruppe.SkillId,
                Name = skillgruppe.Name,
                Description = skillgruppe.Description,
                Mitarbeiter = MapMitarbeiterToMitarbeiterDtos(skillgruppe.Mitarbeiter)
            };

            skillDtos.Add(skillDto);
        }

        return skillDtos;
    }
    
    public IList<MitarbeiterDto> MapMitarbeiterToMitarbeiterDtos(
        IEnumerable<Mitarbeiter> mitarbeiter
    ) {
        var mitarbeiterDtos = new List<MitarbeiterDto>();

        foreach (var angestellter in mitarbeiter)
        {
            var mitarbeiterDto = new MitarbeiterDto
            {
                MitarbeiterId = angestellter.MitarbeiterId,
                Name = angestellter.Name,
            };

            mitarbeiterDtos.Add(mitarbeiterDto);
        }

        return mitarbeiterDtos;
    }
}