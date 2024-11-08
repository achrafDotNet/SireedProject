using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.IP.RepÎP
{
    public class RepIPrepository : IRepIPrepository
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        public RepIPrepository(IHttpContextAccessor iHttpContextAccessor)
        {
            _IHttpContextAccessor = iHttpContextAccessor;
        }

        public async Task<string> GetCountryByIpAsync(string ip)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync($"http://ip-api.com/json/{ip}");
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                return data.country; // Retourne le nom du pays
            }
        }

        public string GetIpAddress()
        {
            var ipAddress = _IHttpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            return ipAddress ?? "Unknown IP";
        }

    
    }
}
