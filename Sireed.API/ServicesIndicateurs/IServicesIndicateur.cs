using Sireed.APPLICATION.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.ServicesIndicateurs
{
    public interface IServicesIndicateur
    {
        Task<List<IndicateurDTO>> GetAsynciNDICATEUR();
    }
}
