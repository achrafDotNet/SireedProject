using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.DTO
{
    public class ThematiqueDTO
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public virtual ICollection<DescriptionThematiqueDTO> DescriptionThematiqueDTOs { get; set; }
    }
}
