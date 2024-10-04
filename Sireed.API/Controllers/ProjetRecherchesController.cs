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
    public class ProjetRecherchesController : Controller
    {
        private readonly AppDbContext _context;

        public ProjetRecherchesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjetRecherches
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProjetRecherches.Include(p => p.Region);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ProjetRecherches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProjetRecherches == null)
            {
                return NotFound();
            }

            var projetRecherche = await _context.ProjetRecherches
                .Include(p => p.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projetRecherche == null)
            {
                return NotFound();
            }

            return View(projetRecherche);
        }

        // GET: ProjetRecherches/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id");
            return View();
        }

        // POST: ProjetRecherches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,Description,Annee,RegionId")] ProjetRecherche projetRecherche)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projetRecherche);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id", projetRecherche.RegionId);
            return View(projetRecherche);
        }

        // GET: ProjetRecherches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProjetRecherches == null)
            {
                return NotFound();
            }

            var projetRecherche = await _context.ProjetRecherches.FindAsync(id);
            if (projetRecherche == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id", projetRecherche.RegionId);
            return View(projetRecherche);
        }

        // POST: ProjetRecherches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Description,Annee,RegionId")] ProjetRecherche projetRecherche)
        {
            if (id != projetRecherche.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projetRecherche);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjetRechercheExists(projetRecherche.Id))
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
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id", projetRecherche.RegionId);
            return View(projetRecherche);
        }

        // GET: ProjetRecherches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjetRecherches == null)
            {
                return NotFound();
            }

            var projetRecherche = await _context.ProjetRecherches
                .Include(p => p.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projetRecherche == null)
            {
                return NotFound();
            }

            return View(projetRecherche);
        }

        // POST: ProjetRecherches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProjetRecherches == null)
            {
                return Problem("Entity set 'AppDbContext.ProjetRecherches'  is null.");
            }
            var projetRecherche = await _context.ProjetRecherches.FindAsync(id);
            if (projetRecherche != null)
            {
                _context.ProjetRecherches.Remove(projetRecherche);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjetRechercheExists(int id)
        {
          return (_context.ProjetRecherches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
