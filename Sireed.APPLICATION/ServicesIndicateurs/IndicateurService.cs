using Sireed.APPLICATION.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sireed.APPLICATION.ServicesIndicateurs
{
    public class IndicateurService : IServicesIndicateur
    {
        private readonly  IServicesIndicateur _servicesIndicateur;
        public IndicateurService(IServicesIndicateur appDb) {
        
          _servicesIndicateur = appDb;
        
        }

        public async Task<List<IndicateurDTO>> GetAsynciNDICATEUR()
        {
            var Indicateurs = await  _servicesIndicateur.GetAsynciNDICATEUR();
            return Indicateurs;
        }
    }
}
