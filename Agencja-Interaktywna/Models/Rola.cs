using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models
{
    public class Rola
    {
        public Rola()
        {
            Rolaosoba = new HashSet<Rolaosoba>();
        }

        public int IdRola { get; set; }

        public string Nazwa { get; set; }

        public virtual ICollection<Rolaosoba> Rolaosoba { get; set; }

    }
}
