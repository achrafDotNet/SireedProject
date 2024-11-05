using Sireed.API.Data;
using Sireed.API.Models;
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
        private readonly AppDbContext _appDbContext;
        public IndicateurService(IRepositoryIndicateurs appDb,AppDbContext context) {

            _repositoryINDICATEUR = appDb;
            _appDbContext = context;
        
        }

        public async Task<List<IndicateurDTO>> GetAsynciNDICATEUR()
        {
            var Indicateurs = await _repositoryINDICATEUR.GetIndicateursAsync();
            return Indicateurs;
        }

        public Task<int> GetNombre()
        {
            var nombreIndicateur = _repositoryINDICATEUR.GetnombreINdicateurs();
            return nombreIndicateur;
        }

        public Task<List<IndicateurDTO>> CalculateAnnualPercentages(List<IndicateurDTO> indicators, int totalYears, int totalRegions)
        {
            // Assuming each indicator has ValeurDTO representing a value for the year and region
            double expectedValue = totalYears * totalRegions; // Change to double for accurate division

            foreach (var indicator in indicators)
            {
                // Calculate percentage if expectedValue is greater than zero
                if (expectedValue > 0) // Avoid division by zero
                {
                    // Calculate percentage based on the actual value and the expected value
                    indicator.PercentageDTO = (indicator.ValeurDTO / expectedValue) * 100;
                }
                else
                {
                    indicator.PercentageDTO = 0;
                }
            }

            // Return the modified list of indicators as a Task
            return Task.FromResult(indicators);
        }

        public Task<List<IndicateurDTO>> CalculerIndicateursAnnuellement(List<Indicateur> indicateurs, int annee)
        {
            var resultats = new List<IndicateurDTO>();

            // Filtrer les indicateurs seulement si une année spécifique est fournie
            var indicateursFiltres = annee > 0 ? indicateurs.Where(i => i.Annee == annee) : indicateurs;

            foreach (var indicateur in indicateursFiltres)
            {
                var valeurCible = ObtenirValeurCible(indicateur); // Méthode pour récupérer la cible
                var pourcentage = valeurCible > 0 ? (indicateur.Valeur / valeurCible) * 100 : 0;

                resultats.Add(new IndicateurDTO
                {
                    RegionNomDTO = indicateur.Region.Nom,
                    IndicateurNomDTO = indicateur.Nom,
                    IndicateurDescriptionDTO = indicateur.Description,
                    SuperficieDTO = indicateur.Region.Superficie,
                    PopulationDTO = indicateur.Region.Population,
                    RegionDescriptionDTO = indicateur.Region.Description,
                    ValeurDTO = indicateur.Valeur,
                    TypeDTO = indicateur.Type,
                    UniteDTO = indicateur.Unite,
                    AnneeDTO = indicateur.Annee,
                    PercentageDTO = pourcentage
                });
            }

            return Task.FromResult(resultats);
        }

        private float ObtenirValeurCible(Indicateur indicateur)
        {
            // Logique pour récupérer la valeur cible (fixe ou dépendante de l'indicateur)
            return 100; // Exemple de valeur cible fixe
        }


        //public Task<List<IndicateurDTO>> CalculateAnnualPercentages(List<IndicateurDTO> indicators, int totalYears, int totalRegions)
        //{
        //    // Assuming each indicator has ValeurDTO representing a value for the year and region
        //    foreach (var indicator in indicators)
        //    {
        //        // Calculate the total expected value based on years and regions
        //        int expectedValue = totalYears * totalRegions; // Change to decimal

        //        // Calculate percentage if ValeurCibleDTO is greater than zero
        //        if (indicator.ValeurDTO > 0) // Avoid division by zero
        //        {
        //            // Calculate percentage based on the actual value and the expected value
        //            indicator.PercentageDTO = (int)(indicator.ValeurDTO / expectedValue) * 100;
        //        }
        //        else
        //        {
        //            indicator.PercentageDTO = 0;
        //        }
        //    }

        //    // Return the modified list of indicators as a Task
        //    return Task.FromResult(indicators);
        //}

        // Task<List<IndicateurDTO>> IServicesIndicateur.CalculateAnnualPercentages(List<IndicateurDTO> indicators)
        //{
        //    foreach (var indicator in indicators)
        //    {
        //        if (indicator.ValeurCibleDTO > 0) // Avoid division by zero
        //        {
        //            indicator.PercentageDTO = (indicator.ValeurDTO / indicator.ValeurCibleDTO) * 100;
        //        }
        //        else
        //        {
        //            indicator.PercentageDTO = 0;
        //        }
        //    }
        //    return Task.FromResult(indicators); // Return the result as a Task
        //}
    }
}
