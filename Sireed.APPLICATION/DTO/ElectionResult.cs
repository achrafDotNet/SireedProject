using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.DTO
{
    public class ElectionResult
    {
        public string DistrictName { get; set; }
        public string WinningParty { get; set; }
        public int TotalVotes { get; set; }
        public double[][] Coordinates { get; set; }  // Coordinates for GeoJSON (simplified)
    }
}
