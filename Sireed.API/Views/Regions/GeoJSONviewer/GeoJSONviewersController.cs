using Microsoft.AspNetCore.Mvc;
using Sireed.APPLICATION.DTO;

namespace Sireed.API.Views.Regions.GeoJSONviewer
{
    public class GeoJSONviewersController : Controller
    {
        public IActionResult GeoJSON()
        {
            var regions = new List<RegionDTO>

        {

            new RegionDTO

            {

                Id = 1,

                NomDTO = "Rabat-Salé-Kénitra",

                DescriptionDTO = "Description de la région Rabat-Salé-Kénitra.",

                SuperficieDTO = 18000, // Superficie en km²

                PopulationDTO = 3000000, // Population

                // Remplissez les autres propriétés si nécessaire

            }

            // Ajoutez d'autres régions si nécessaire

        };


            return View(regions);
        }
    }
}
