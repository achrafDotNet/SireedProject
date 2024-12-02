using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.DTO
{
    public class ThematiqueDTO
    {
        public int Id { get; set; }
        [Display(Name = "Déchets")]
        public string DechetsDTO { get; set; } = string.Empty;
        public string EauEtassainissementDTO { get; set; } = string.Empty;
        public string LittoralEtbiodiversitéDTO { get; set; } = string.Empty;
        public string AgricultureEtindustrieDTO { get; set; } = string.Empty;
        public string PopulationEtEducationEnvironnementaleDTO { get; set; } = string.Empty;
        public string AirDTO { get; set; } = string.Empty;
        public ICollection<IndicateurDTO> indicateurDTOs { get; set; }
    }
}
