using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.IP.RepÎP
{
    public interface IRepIPrepository
    {
        string GetIpAddress();
        Task<string> GetCountryByIpAsync(string ip);

    }
}
