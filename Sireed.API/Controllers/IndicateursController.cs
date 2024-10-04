using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sireed.API.Data;
using Sireed.API.Models;

namespace Sireed.API.Controllers
{
    public class IndicateursController : Controller
    {
        private readonly AppDbContext _context;

        public IndicateursController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Indicateurs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.indicateurs.Include(i => i.Region);
            return View(await appDbContext.ToListAsync());
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
