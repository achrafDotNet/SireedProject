using Microsoft.EntityFrameworkCore;
using Sireed.API.Data;
using Sireed.APPLICATION.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.INFRASTRUCTURE.RepositoryIndicateurs
{
    public class RepositoryIndicateur : IRepositoryIndicateurs
    {
        private readonly AppDbContext _context;
        public RepositoryIndicateur(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<List<IndicateurDTO>> GetIndicateursAsync()
        {
            return await _context.indicateurs
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
        }

       async Task<List<RegionCountDTO>> IRepositoryIndicateurs.GetnombreINdicateurs()
        {
            var results = await _context.indicateurs
                .GroupBy(i => i.RegionId)
                .Select(g => new RegionCountDTO
                {
                    Nombre = g.Count()
                })
                .ToListAsync();

            return results; // Now returns the List<RegionCountDTO>
        }
    }
}
