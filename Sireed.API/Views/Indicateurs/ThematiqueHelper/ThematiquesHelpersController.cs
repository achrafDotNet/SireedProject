using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sireed.API.Views.Indicateurs.ThematiqueHelper
{
    public class ThematiquesHelpersController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Dechets()
        {
            return View();

        }
        public IActionResult Eauetassainissement()
        {
            return View();
        }

        public IActionResult Littoraletbiodiversite() => View();
     
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
