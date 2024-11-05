using Sireed.APPLICATION.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.INFRASTRUCTURE.RepositoryIndicateurs
{
    public interface IRepositoryIndicateurs
    {
        Task<List<IndicateurDTO>> GetIndicateursAsync();
        Task<int> GetnombreINdicateurs(); 

    }
}

