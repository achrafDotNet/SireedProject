using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.DTO
{
    public class IndicateurDTO
    {
        public string RegionNomDTO { get; set; }
        public string IndicateurNomDTO { get; set; }
        public string IndicateurDescriptionDTO { get; set; }
        public double SuperficieDTO { get; set; }
        public int PopulationDTO { get; set; }
        public string RegionDescriptionDTO { get; set; }
        public double ValeurDTO { get; set; }
        public string TypeDTO { get; set; }
        public string UniteDTO { get; set; }
        public int AnneeDTO { get; set; }

        // existing properties
        public double PercentageDTO { get; set; }

        //VALEUR CIBLE 

        public int ValeurCibleDTO { get;  set; } 

    }
}
// Calcul du pourcentage de la barre de progression
/* public int ProgressPercentage => (int)(ValeurDTO * 100 / PopulationDTO);*/ // Ex. basé sur Population

// Calcul du pourcentage de la barre de progression basé sur AnneeDTO
//public int ProgressPercentage
//{
//    get
//    {
//        // Éviter la division par zéro
//        if (AnneeDTO == 0)
//        {
//            return 0; // ou une autre valeur par défaut appropriée
//        }
//        return (int)(ValeurDTO * 100 / AnneeDTO); // Ex. basé sur AnneeDTO
//    }
//}
// Calcul du pourcentage de la barre de progression basé sur le nombre d'indicateurs