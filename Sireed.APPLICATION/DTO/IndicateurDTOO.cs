using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.DTO
{
    public class IndicateurDTOO
    {
        public int Id { get; set; }
        public string NomDTO { get; set; }
        public string DescriptionDTO { get; set; }
        public float ValeurDTO { get; set; }
        public int AnneeDTO { get; set; }
        public string TypeDTO { get; set; }
        public string UniteDTO { get; set; }

        public int RegionId { get; set; }
        public RegionDTO RegionDTO { get; set; }
    }
}
