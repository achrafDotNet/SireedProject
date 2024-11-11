using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.DTO
{
    public class RegionDTO
    {
        public int Id { get; set; }
        public string NomDTO { get; set; }
        public string DescriptionDTO { get; set; }
        public float SuperficieDTO { get; set; }
        public int PopulationDTO { get; set; }

        public ICollection<CommuneDTO> CommunesDTO { get; set; } = new List<CommuneDTO>();
        public List<IndicateurDTOO> IndicateursDTOO { get; set; }
        public ICollection<ProjetRechercheDTO> ProjetsRechercheDTO { get; set; }
        public ICollection<DocumentDTO> DocumentsDTO { get; set; }
        public ICollection<PartenaireDTO> PartenairesDTO { get; set; }
        public ICollection<ActualiteDTO> ActualiteDTO { get; set; }

    }
}
