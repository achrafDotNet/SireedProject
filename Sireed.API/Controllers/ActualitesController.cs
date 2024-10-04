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
    public class ActualitesController : Controller
    {
        private readonly AppDbContext _context;

        public ActualitesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Actualites
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Actualites.Include(a => a.Region);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Actualites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Actualites == null)
            {
                return NotFound();
            }

            var actualite = await _context.Actualites
                .Include(a => a.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actualite == null)
            {
                return NotFound();
            }

            return View(actualite);
        }

        // GET: Actualites/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id");
            return View();
        }

        // POST: Actualites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,Description,DatePublication,RegionId")] Actualite actualite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actualite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id", actualite.RegionId);
            return View(actualite);
        }

        // GET: Actualites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actualites == null)
            {
                return NotFound();
            }

            var actualite = await _context.Actualites.FindAsync(id);
            if (actualite == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id", actualite.RegionId);
            return View(actualite);
        }

        // POST: Actualites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Description,DatePublication,RegionId")] Actualite actualite)
        {
            if (id != actualite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actualite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActualiteExists(actualite.Id))
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
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id", actualite.RegionId);
            return View(actualite);
        }

        // GET: Actualites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actualites == null)
            {
                return NotFound();
            }

            var actualite = await _context.Actualites
                .Include(a => a.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actualite == null)
            {
                return NotFound();
            }

            return View(actualite);
        }

        // POST: Actualites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actualites == null)
            {
                return Problem("Entity set 'AppDbContext.Actualites'  is null.");
            }
            var actualite = await _context.Actualites.FindAsync(id);
            if (actualite != null)
            {
                _context.Actualites.Remove(actualite);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActualiteExists(int id)
        {
          return (_context.Actualites?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
