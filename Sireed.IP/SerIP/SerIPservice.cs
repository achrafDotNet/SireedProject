using Newtonsoft.Json;
using Sireed.IP.RepÎP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.IP.SerIP
{
    public class SerIPservice : ISerIPservice
    {
        private readonly IRepIPrepository _repIPrepository;
        public SerIPservice(IRepIPrepository repIPrepository)
        {
            _repIPrepository = repIPrepository;
        }
        public async Task LogIpAddressAsync()
        {
            // Récupérer l'IP du visiteur
            string ipAddress = _repIPrepository.GetIpAddress();

            // Obtenir le pays correspondant à l'IP
            string country = await _repIPrepository.GetCountryByIpAsync(ipAddress);

            // Définir le chemin vers le fichier JSON
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "ip_log.json");
            string directoryPath = Path.GetDirectoryName(filePath);

            // Créer le dossier Logs s'il n'existe pas
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            List<dynamic> logData;

            if (File.Exists(filePath))
            {
                string existingData = await File.ReadAllTextAsync(filePath);

                // Si le fichier est vide ou ne contient pas un tableau, initialisez-le en tant que tableau vide
                if (string.IsNullOrWhiteSpace(existingData) || !existingData.TrimStart().StartsWith("["))
                {
                    logData = new List<dynamic>();
                }
                else
                {
                    logData = JsonConvert.DeserializeObject<List<dynamic>>(existingData);
                }
            }
            else
            {
                logData = new List<dynamic>();
            }

            // Ajouter une nouvelle entrée
            logData.Add(new { IpAddress = ipAddress, Country = country, Timestamp = DateTime.UtcNow });

            // Sauvegarder les données dans le fichier JSON
            string jsonData = JsonConvert.SerializeObject(logData, Formatting.Indented);
            await File.WriteAllTextAsync(filePath, jsonData);
            //// Récupérer l'IP du visiteur
            //string ipAddress = _repIPrepository.GetIpAddress();

            //// Obtenir le pays correspondant à l'IP
            //string country = await _repIPrepository.GetCountryByIpAsync(ipAddress);

            //// Charger ou créer le fichier JSON
            //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "ip_log.json");
            //List<dynamic> logData = new List<dynamic>();

            //if (File.Exists(filePath))
            //{
            //    // Charger le contenu existant du fichier JSON
            //    string existingData = await File.ReadAllTextAsync(filePath);
            //    logData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(existingData);
            //}

            //// Ajouter une nouvelle entrée
            //logData.Add(new { IpAddress = ipAddress, Country = country, Timestamp = DateTime.UtcNow });

            //// Sauvegarder les données dans le fichier JSON
            //string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(logData, Newtonsoft.Json.Formatting.Indented);
            //await File.WriteAllTextAsync(filePath, jsonData);

        }
    }
}
