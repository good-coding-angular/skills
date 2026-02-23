using skills_be.Models;
using skills_be.Models.Dto;

namespace skills_be.Mapper;

public class MitarbeiterMapper
{
    public MitarbeiterDto MapSingleMitarbeiterToMitarbeiterDto(
        Mitarbeiter mitarbeiter
    ) {
        return new MitarbeiterDto
        {
            MitarbeiterId = mitarbeiter.MitarbeiterId,
            Name = mitarbeiter.Name,
            Available = mitarbeiter.Available
        };
    }
    
    public IList<MitarbeiterDto> MapMitarbeiterToMitarbeiterDto(
        IEnumerable<Mitarbeiter> mitarbeiter
    ) {
        var mitarbeiterDtos = new List<MitarbeiterDto>();

        foreach (var angestellter in mitarbeiter)
        {
            var mitarbeiterDto = new MitarbeiterDto
            {
                MitarbeiterId = angestellter.MitarbeiterId,
                Name = angestellter.Name,
                Available = angestellter.Available
            };

            mitarbeiterDtos.Add(mitarbeiterDto);
        }

        return mitarbeiterDtos;
    }
}