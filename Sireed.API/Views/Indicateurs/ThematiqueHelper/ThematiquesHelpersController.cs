using Microsoft.AspNetCore.Mvc;

namespace Sireed.API.Views.Indicateurs.ThematiqueHelper
{
    public class ThematiquesHelpersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dechets()
        {
            return View();

        }
        public IActionResult Eauetassainissement()
        {
            return View();
        }

        public IActionResult Littoraletbiodiversite()
        {
            return View();
        }
        public IActionResult Agricultureetindustrie()
        {
            return View();
        }
        public IActionResult Populationeteducationenvironnementale()
        {
            return View();
        }
        public IActionResult Air()
        {
            return View();
        }
    }
}
