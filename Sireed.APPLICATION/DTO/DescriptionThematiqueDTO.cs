using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sireed.APPLICATION.DTO
{
    public class DescriptionThematiqueDTO
    {
        public int Id { get; set; }

        public int TheMatId { get; set; }

        public string DescriptionText { get; set; }


        public virtual ThematiqueDTO ThematiqueDTO { get; set; }
    }
}
