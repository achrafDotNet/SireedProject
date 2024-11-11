using Sireed.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.DOMAIN.Models
{
    public class Commune
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Population { get; set; }

        // Clé étrangère pour la région
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
