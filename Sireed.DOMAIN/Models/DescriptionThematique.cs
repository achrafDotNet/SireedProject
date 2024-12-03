using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.DOMAIN.Models
{
    public class DescriptionThematique
    {
        public int Id { get; set; }

        public int TheMatId { get; set; }

        public string DescriptionText { get; set; }


        public virtual Thematique Thematique { get; set; }
    }
}
