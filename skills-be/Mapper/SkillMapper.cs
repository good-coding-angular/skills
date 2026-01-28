using skills_be.Models;
using skills_be.Models.Dto;

namespace skills_be.Mapper;

public class SkillMapper
{
    public IList<SkillDto> MapSkillToSkillDto(
        IEnumerable<Skill> skills
    ) {
        var skillDtos = new List<SkillDto>();

        foreach (var skill in skills)
        {
            var skillDto = new SkillDto
            {
                SkillId = skill.SkillId,
                Name = skill.Name,
                Description = skill.Description,
                Skillgruppen = MapSkillGruppenToSkillGruppenDtos(skill.Skillgruppen)
            };

            skillDtos.Add(skillDto);
        }

        return skillDtos;
    }
    
    public IList<SkillgruppeDto> MapSkillGruppenToSkillGruppenDtos(
        IEnumerable<Skillgruppe> skillGruppen
    ) {
        var skillgruppeDtos = new List<SkillgruppeDto>();

        foreach (var skillgruppe in skillGruppen)
        {
            var skillgruppeDto = new SkillgruppeDto
            {
                SkillGruppeId = skillgruppe.SkillGruppeId,
                Name = skillgruppe.Name,
            };

            skillgruppeDtos.Add(skillgruppeDto);
        }

        return skillgruppeDtos;
    }
}