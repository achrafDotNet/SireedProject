using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace Sireed.API.Controllers
{
    public class MapController : Microsoft.AspNetCore.Mvc.Controller
    {
        public MapController()
        {
                
        }
        public IActionResult GetRegionData()
        {
            var regionData = new
            {
                Bounds = new[]
                {
                new { lat = 34.033, lng = -6.832 }, // Rabat
                new { lat = 33.992, lng = -6.866 }, // Salé
                new { lat = 34.258, lng = -6.579 }  // Kénitra
            },
                Filters = new[] { "Population", "Industrie", "Transport" }
            };

            // Retourner les données au format JSON
            return Ok(regionData);
        }
    }
}
