using Sireed.APPLICATION.DTO;
using Sireed.INFRASTRUCTURE.RepositoryIndicateurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sireed.APPLICATION.ServicesIndicateurs
{
    public class IndicateurService : IServicesIndicateur
    {
        private readonly IRepositoryIndicateurs _repositoryINDICATEUR;
        public IndicateurService(IRepositoryIndicateurs appDb) {

            _repositoryINDICATEUR = appDb;
        
        }

        public async Task<List<IndicateurDTO>> GetAsynciNDICATEUR()
        {
            var Indicateurs = await _repositoryINDICATEUR.GetIndicateursAsync();
            return Indicateurs;
        }

     

         Task<List<IndicateurDTO>> IServicesIndicateur.CalculateAnnualPercentages(List<IndicateurDTO> indicators)
        {
            foreach (var indicator in indicators)
            {
                if (indicator.ValeurCibleDTO > 0) // Avoid division by zero
                {
                    indicator.PercentageDTO = (indicator.ValeurDTO / indicator.ValeurCibleDTO) * 100;
                }
                else
                {
                    indicator.PercentageDTO = 0;
                }
            }
            return Task.FromResult(indicators); // Return the result as a Task
        }
    }
}
