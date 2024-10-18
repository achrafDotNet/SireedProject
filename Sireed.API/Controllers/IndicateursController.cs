using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        //public readonly IndicateurService _serviceiNDICATEUR;
        private readonly IRepositoryIndicateurs _repositoryIndicateurs;

        public IndicateursController(AppDbContext context/*,IndicateurService indicateurService*/,IRepositoryIndicateurs repositoryIndicateurs)
        {
            _context = context;
            //_serviceiNDICATEUR = indicateurService;
            _repositoryIndicateurs = repositoryIndicateurs;
        }

        // GET: Indicateurs
        // GET: Indicateurs
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _repositoryIndicateurs.GetIndicateursAsync());
            //var regionIndicateurs = await _context.indicateurs
            //                          .Join(_context.Regions,
            //                                i => i.RegionId,
            //                                r => r.Id,
            //                                (i, r) => new IndicateurDTO
            //                                {
            //                                    RegionNomDTO = r.Nom,
            //                                    IndicateurNomDTO = i.Nom,
            //                                    IndicateurDescriptionDTO = i.Description,
            //                                    SuperficieDTO = r.Superficie,
            //                                    PopulationDTO = r.Population,
            //                                    RegionDescriptionDTO = r.Description,
            //                                    ValeurDTO = (decimal)i.Valeur,
            //                                    TypeDTO = i.Type,
            //                                    UniteDTO = i.Unite,
            //                                    AnneeDTO = i.Annee
            //                                })
            //                          .ToListAsync();

            //// Vérifiez si le modèle est vide pour déboguer
            //if (!regionIndicateurs.Any())
            //{
            //    // Logique pour gérer le cas où il n'y a pas d'indicateurs
            //    // Par exemple, vous pourriez vouloir retourner une vue avec un message d'erreur
            //}

            //return View(regionIndicateurs);
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
                                               ValeurDTO = (decimal)i.Valeur,
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
