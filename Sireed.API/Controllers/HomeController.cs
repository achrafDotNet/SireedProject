using Microsoft.AspNetCore.Mvc;
using Sireed.API.Models;
using Sireed.APPLICATION.DTO;
using Sireed.IP.SerIP;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Web.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace Sireed.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISerIPservice _serIPservice;
        public HomeController(ILogger<HomeController> logger,ISerIPservice serIPservice)
        {
            _logger = logger;
            _serIPservice = serIPservice;
        }

        public async Task<IActionResult> Index()
        {
            await _serIPservice.LogIpAddressAsync();
            return View();
        }

        public IActionResult Oriental()
        {
             return View();
        }

		//public IActionResult Map()
		//{
		//    return View();
		//}
		// Simulating election results
		public IActionResult Map()
        {
            var electionResults = new List<ElectionResult>
    {
        new ElectionResult
        {
            DistrictName = "District 1",
            WinningParty = "Party A",
            TotalVotes = 55000,
            Coordinates = new double[][] {
                new double[] { -13.16708946, 27.68309784 },
                new double[] { -13.05010033, 27.75125313 },
                new double[] { -13.16708946, 27.68309784 }
            }
        },
        new ElectionResult
        {
            DistrictName = "District 2",
            WinningParty = "Party B",
            TotalVotes = 48000,
            Coordinates = new double[][] {
                new double[] { -8.676, 31.599 },
                new double[] { -8.449, 31.922 },
                new double[] { -8.676, 31.599 }
            }
        }
    };

            var geoJson = new
            {
                type = "FeatureCollection",
                features = electionResults.Select(r => new
                {
                    type = "Feature",
                    geometry = new
                    {
                        type = "Polygon",
                        coordinates = new[] { r.Coordinates }
                    },
                    properties = new
                    {
                        district = r.DistrictName,
                        winner = r.WinningParty,
                        votes = r.TotalVotes
                    }
                })
            };

            return Json(geoJson);  // System.Text.Json is used automatically
        }

        public IActionResult GetProperties()
        {
            var properties = new List<Property>
    {
        new Property { Id = 1, Name = "COCO", Price = 205.50, Latitude = 51.505, Longitude = -0.09 },
        new Property { Id = 2, Name = "Maison de Plage", Price = 350.00, Latitude = 51.51, Longitude = -0.1 }
        // Ajoutez d'autres propriétés ici...
    };

            return Json(properties);
        }



        public IActionResult SireedHome()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
