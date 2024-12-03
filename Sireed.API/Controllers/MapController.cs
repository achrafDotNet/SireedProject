using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sireed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : Controller
    {

        [HttpGet("region-data")]
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

        [HttpGet]
        public IActionResult Index() {
        
             return View();
        
        }
    }
}
