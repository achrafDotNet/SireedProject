using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.DTO
{
    public class PartenaireDTO
    {
        public int Id { get; set; }
        public string NomDTO { get; set; }
        public string DescriptionDTO { get; set; }
        public string ContactDTO { get; set; }

        public int RegionId { get; set; }
        public RegionDTO Region { get; set; }
    }
}
