using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sireed.API.Views.Indicateurs.ThematiqueHelper.NavigationTableBoards;
using Sireed.APPLICATION.DTO;

namespace Sireed.API.Views.Indicateurs.ThematiqueHelper
{
    public class ThematiquesHelpersController : Controller
    {
        private readonly INavigation _navigation;
        public ThematiquesHelpersController(INavigation navigation) { 
          
            _navigation = navigation;
        }

        public IActionResult TMA(ThematiqueDTO thematiqueDTO) {

            return View(_navigation.GetAllThematiques(thematiqueDTO));

        }
     
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
