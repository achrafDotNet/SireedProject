using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.DTO
{
    public class CommuneDTO
    {
        public int Id { get; set; }
        public string NomDTO { get; set; }
        public int PopulationDTO { get; set; }

        // Clé étrangère pour la région
        public int RegionId { get; set; }
        public RegionDTO RegionDTO { get; set; }
    }
}

