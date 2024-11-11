using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.DTO
{
    public class ActualiteDTO
    {
        public int Id { get; set; }
        public string TitreDTO { get; set; }
        public string DescriptionDTO { get; set; }
        public DateTime DatePublicationDTO { get; set; }

        public int RegionId { get; set; }
        public RegionDTO Region { get; set; }
    }
}
