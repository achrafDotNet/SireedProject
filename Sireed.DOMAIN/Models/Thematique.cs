using Sireed.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.DOMAIN.Models
{
    public class Thematique
    {

        public int Id { get; set; }

        public string Nom { get; set; }

        public virtual ICollection<DescriptionThematique> Descriptions { get; set; }
        //public int Id { get; set; }
        //public string Dechets { get; set; } = string.Empty;
        //public string EauEtassainissement { get; set; } = string.Empty;
        //public string LittoralEtbiodiversité { get; set; } = string.Empty;
        //public string AgricultureEtindustrie { get; set; } = string.Empty;
        //public string PopulationEtEducationEnvironnementale { get; set; } = string.Empty;
        //public string Air { get; set; } = string.Empty;
        //public ICollection<Indicateur> Indicateurs { get; set; }
    }
}
