using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models
{
    public class Rolaosoba
    {

        public int IdRola { get; set; }

        public int IdOsoba { get; set; }

        public virtual Rola IdrolaNavigation { get; set; }
        public virtual Osoba IdosobaNavigation { get; set; }

    }
}
