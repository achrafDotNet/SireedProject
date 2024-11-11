using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sireed.API.Data;
using Sireed.API.Models;
using Sireed.APPLICATION.DTO;
using Sireed.APPLICATION.ServicesIndicateurs;
using Sireed.INFRASTRUCTURE.RepositoryIndicateurs;

namespace Sireed.API.Controllers
{
    public class IndicateursController : Controller
    {
        private readonly AppDbContext _context;
        public readonly IServicesIndicateur _serviceiNDICATEUR;
        private readonly IRepositoryIndicateurs _repositoryIndicateurs;

        public IndicateursController(AppDbContext context, IServicesIndicateur indicateurService, IRepositoryIndicateurs repositoryIndicateurs)
        {
            _context = context;
            _serviceiNDICATEUR = indicateurService;
            _repositoryIndicateurs = repositoryIndicateurs;
        }


        // GET: Indicateurs
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    // Define the total number of years and regions for your calculations
        //    var totalYears = await GetTotalYears(); // Get the total number of years
        //    var totalRegions = await GetTotalRegions(); // Get the total number of regions

        //    // Await the task to get the actual list of IndicateurDTO
        //    List<IndicateurDTO> indicateurs = await _serviceiNDICATEUR.GetAsynciNDICATEUR();
        //    List<IndicateurDTO> perc = await _serviceiNDICATEUR.CalculateAnnualPercentages(indicateurs, totalYears, totalRegions);

        //    // Pass the list to the view
        //    return View(perc);
        //}


        // Action pour afficher les indicateurs pour une année donnée
        [HttpGet]

        public async Task<IActionResult> Index(int annee)
        {
            // Récupérer tous les indicateurs avec leurs régions associées
            var indicateurs = _context.indicateurs.Include(i => i.Region).ToList();

            // Calculer les pourcentages pour chaque indicateur
            var indicateurDTOs = await _serviceiNDICATEUR.CalculerIndicateursAnnuellement(indicateurs, annee: 0); // annee: 0 pour ignorer le filtrage par année

            // Passer les données à la vue
            return View(indicateurDTOs);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetNombreIndic()
        //{
        //    var nmbrIndic = await _serviceiNDICATEUR.GetNombre();
        //    return Json(nmbrIndic);
        //}

        //[HttpGet]
        //public IActionResult GetPopulationData(string regionNom)
        //{
        //    // Fetch population data using a dedicated method
        //    var populationData = GetPopulationFromDatabase(regionNom);

        //    // Fetch chart data using a dedicated method
        //    var populationChartData = GetPopulationChartData(regionNom);

        //    return Json(new { populationData, populationChartData });
        //}



        // Method to fetch population data from the database
        //private IEnumerable<object> GetPopulationFromDatabase(string regionNom)
        //{
        //    // Retrieve the region based on the provided name
        //    var region = _context.Regions.FirstOrDefault(r => r.Nom == regionNom);
        //    if (region == null)
        //    {
        //        return Enumerable.Empty<object>(); // Return empty if region not found
        //    }

        //    // Get population data related to the region
        //    return _context.Actualites
        //        .Where(a => a.RegionId == region.Id)
        //        .Select(a => new
        //        {
        //            commune = a.Titre, // Adjust according to your data model
        //            population = a.Description // Adjust according to your data model
        //        })
        //        .ToList();
        //}

        // GET: Regions
        public async Task<IActionResult> ChartModel(DateTime? startDate, DateTime? endDate)
        {
            var regions = await _context.Regions
                .Include(r => r.Indicateurs)
                .ToListAsync();

            if (startDate.HasValue && endDate.HasValue)
            {
                foreach (var region in regions)
                {
                    region.Indicateurs = region.Indicateurs
                        .Where(i => i.Annee >= startDate.Value.Year && i.Annee <= endDate.Value.Year)
                        .ToList();
                }
            }

            var regionDTOs = regions.Select(r => new RegionDTO
            {
                Id = r.Id,
                NomDTO = r.Nom,
                DescriptionDTO = r.Description,
                SuperficieDTO = r.Superficie,
                PopulationDTO = r.Population,
                IndicateursDTOO = r.Indicateurs.Select(i => new IndicateurDTOO  // Assurez-vous que c'est IndicateurDTO
                {
                    Id = i.Id,
                    NomDTO = i.Nom,
                    ValeurDTO = i.Valeur,
                    AnneeDTO = i.Annee
                }).ToList() // Ceci retounera une List<IndicateurDTO>
            }).ToList(); // Vous pouvez les transformer en liste ici si vous avez besoin d'une liste.

            return View(regionDTOs);
        }




        [HttpGet]
        public IActionResult GetPopulationData(string regionNom, int year)
        {
            // Récupérer les données de population et du graphique
            var populationData = GetPopulationFromDatabase(regionNom);
            var populationChartData = GetPopulationChartData(regionNom, year);

            return Json(new { populationData, populationChartData });
        }

        private IEnumerable<object> GetPopulationFromDatabase(string regionNom)
        {
            var region = _context.Regions.FirstOrDefault(r => r.Nom == regionNom);
            if (region == null)
            {
                return Enumerable.Empty<object>(); // Vérifiez si la région est trouvée
            }

            var communes = _context.Communes
                .Where(c => c.RegionId == region.Id)
                .Select(c => new
                {
                    commune = c.Nom,
                    population = c.Population
                })
                .ToList();

            // Ajoutez un log pour vérifier les données
            Console.WriteLine($"Found {communes.Count} communes in the region: {regionNom}");

            return communes;
        }

        private object GetPopulationChartData(string regionNom, int year)
        {
            var region = _context.Regions.FirstOrDefault(r => r.Nom == regionNom);
            if (region == null)
            {
                return new { labels = Array.Empty<string>(), data = Array.Empty<int>() };
            }

            var data = _context.Communes
                .Where(c => c.RegionId == region.Id)
                .SelectMany(c => _context.indicateurs
                    .Where(i => i.Annee == year && i.RegionId == region.Id)
                    .Select(i => new { commune = c.Nom, population = i.Valeur })
                )
                .ToList();

            // Vérifiez si les données du graphique sont récupérées
            Console.WriteLine($"Found {data.Count} data points for year {year}");

            return new
            {
                labels = data.Select(d => d.commune).ToArray(),
                data = data.Select(d => d.population).ToArray()
            };
        }



        //[HttpGet]
        //public IActionResult GetPopulationData(string regionNom, int year)
        //{
        //    // Récupérer les données de population et du graphique
        //    var populationData = GetPopulationFromDatabase(regionNom);
        //    var populationChartData = GetPopulationChartData(regionNom, year);

        //    return Json(new { populationData, populationChartData });
        //}

        //private IEnumerable<object> GetPopulationFromDatabase(string regionNom)
        //{
        //    var region = _context.Regions.FirstOrDefault(r => r.Nom == regionNom);
        //    if (region == null)
        //    {
        //        return Enumerable.Empty<object>();
        //    }

        //    // Récupérer les données de population pour chaque commune associée à la région
        //    return _context.Communes
        //        .Where(c => c.RegionId == region.Id)
        //        .Select(c => new
        //        {
        //            commune = c.Nom,
        //            population = c.Population
        //        })
        //        .ToList();
        //}
        //private object GetPopulationChartData(string regionNom, int year)
        //{
        //    var region = _context.Regions.FirstOrDefault(r => r.Nom == regionNom);
        //    if (region == null)
        //    {
        //        return new { labels = Array.Empty<string>(), data = Array.Empty<int>() };
        //    }

        //    var data = _context.Communes
        //        .Where(c => c.RegionId == region.Id)
        //        .SelectMany(c => _context.indicateurs
        //            .Where(i => i.Annee == year && i.RegionId == region.Id)
        //            .Select(i => new { commune = c.Nom, population = i.Valeur })
        //        )
        //        .ToList();

        //    return new
        //    {
        //        labels = data.Select(d => d.commune).ToArray(),
        //        data = data.Select(d => d.population).ToArray()
        //    };
        //}



        //private object GetPopulationChartData(string regionNom)
        //{
        //    var region = _context.Regions.FirstOrDefault(r => r.Nom == regionNom);
        //    if (region == null)
        //    {
        //        return new { labels = Array.Empty<string>(), data = Array.Empty<int>() }; // Return empty data if region not found
        //    }

        //    var data = _context.indicateurs
        //        .Where(i => i.RegionId == region.Id)
        //        .GroupBy(i => i.Annee)
        //        .Select(g => new
        //        {
        //            Annee = g.Key,
        //            Valeur = g.Sum(i => i.Valeur) // Sum values per year
        //        })
        //        .ToList();

        //    return new
        //    {
        //        labels = data.Select(d => d.Annee.ToString()).ToArray(),
        //        data = data.Select(d => d.Valeur).ToArray()
        //    };
        //}

        // Exemple de méthode pour obtenir les données du graphique

        //private async Task<List<Region>> GetTotalRegionsAsync()

        //{

        //    // Remplacez cette logique par celle qui correspond à vos besoins

        //    return await _context.Regions.ToListAsync(); // Retourner toutes les régions

        //}



        // GET: Indicateurs
        private async Task<int> GetTotalYears()
        {
            // Replace this with your actual logic to get the total number of years
            // Assuming you have a property that indicates a year for each indicator
            return await _context.indicateurs.Select(i => i.Annee).Distinct().CountAsync();
        }

        private async Task<List<Region>> GetTotalRegionsAsync()
        {
            return await _context.Regions.Include(r => r.Indicateurs).ToListAsync(); // Inclut les indicateurs dans la requête
        }


        //private async Task<IActionResult> GetTotalRegions()
        //{
        //    // Replace this with your actual logic to get the total number of regions
        //    return Ok( await _context.Regions.CountAsync());
        //}



        [HttpGet]
        public async Task<IActionResult> GetIndicateursData()
        {
            var indicateurs = await _repositoryIndicateurs.GetIndicateursAsync();
            return Json(indicateurs);
        }



        // GET: Indicateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.indicateurs == null)
            {
                return NotFound();
            }

            var indicateur = await _context.indicateurs
                .Include(i => i.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indicateur == null)
            {
                return NotFound();
            }

            return View(indicateur);
        }

        // GET: Indicateurs/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id");
            return View();
        }

        public async Task<IActionResult> ListIndicateurs()
        {
            var regionIndicateurs = await _context.indicateurs
                                     .Join(_context.Regions,
                                           i => i.RegionId,
                                           r => r.Id,
                                           (i, r) => new IndicateurDTO
                                           {
                                               RegionNomDTO = r.Nom,
                                               IndicateurNomDTO = i.Nom,
                                               IndicateurDescriptionDTO = i.Description,
                                               SuperficieDTO = r.Superficie,
                                               PopulationDTO = r.Population,
                                               RegionDescriptionDTO = r.Description,
                                               ValeurDTO = (int)i.Valeur,
                                               TypeDTO = i.Type,
                                               UniteDTO = i.Unite,
                                               AnneeDTO = i.Annee
                                           })
                                     .ToListAsync();

            // Vérifiez si le modèle est vide pour déboguer
            if (!regionIndicateurs.Any())
            {
                // Logique pour gérer le cas où il n'y a pas d'indicateurs
                // Par exemple, vous pourriez vouloir retourner une vue avec un message d'erreur
            }

            return View(regionIndicateurs);
        }

        // POST: Indicateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,Valeur,Annee,Type,Unite,RegionId")] Indicateur indicateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(indicateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id", indicateur.RegionId);
            return View(indicateur);
        }

        // GET: Indicateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.indicateurs == null)
            {
                return NotFound();
            }

            var indicateur = await _context.indicateurs.FindAsync(id);
            if (indicateur == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id", indicateur.RegionId);
            return View(indicateur);
        }

        // POST: Indicateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Valeur,Annee,Type,Unite,RegionId")] Indicateur indicateur)
        {
            if (id != indicateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(indicateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndicateurExists(indicateur.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id", indicateur.RegionId);
            return View(indicateur);
        }

        // GET: Indicateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.indicateurs == null)
            {
                return NotFound();
            }

            var indicateur = await _context.indicateurs
                .Include(i => i.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indicateur == null)
            {
                return NotFound();
            }

            return View(indicateur);
        }

        // POST: Indicateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.indicateurs == null)
            {
                return Problem("Entity set 'AppDbContext.indicateurs'  is null.");
            }
            var indicateur = await _context.indicateurs.FindAsync(id);
            if (indicateur != null)
            {
                _context.indicateurs.Remove(indicateur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndicateurExists(int id)
        {
          return (_context.indicateurs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
