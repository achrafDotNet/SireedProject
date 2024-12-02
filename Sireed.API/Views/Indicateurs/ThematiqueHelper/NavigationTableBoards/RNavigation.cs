
using Microsoft.EntityFrameworkCore;
using Sireed.API.Data;
using Sireed.APPLICATION.DTO;
using Sireed.DOMAIN.Models;

namespace Sireed.API.Views.Indicateurs.ThematiqueHelper.NavigationTableBoards
{
    public class RNavigation : INavigation
    {
        private readonly AppDbContext _appDbContext;
        public RNavigation(AppDbContext appDbContext) {
        
            _appDbContext = appDbContext;
        
        }

        public Task GetAllThematiques(ThematiqueDTO thematique)
        {
            var TMA = new Thematique()
            {
                Dechets = thematique.DechetsDTO,
                EauEtassainissement = thematique.EauEtassainissementDTO, 
                LittoralEtbiodiversité = thematique.LittoralEtbiodiversitéDTO,
                AgricultureEtindustrie = thematique.AgricultureEtindustrieDTO,
                PopulationEtEducationEnvironnementale = thematique.PopulationEtEducationEnvironnementaleDTO,
                Air = thematique.AirDTO
            };
            return Task.FromResult(TMA);
        }

    }
}
