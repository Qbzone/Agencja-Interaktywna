using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Klient
    {
        public Klient()
        {
            KlientFirma = new HashSet<KlientFirma>();
            PracownikKlient = new HashSet<PracownikKlient>();
        }

        public int IdKlient { get; set; }
        public string Priorytet { get; set; }

        public virtual Osoba IdKlientNavigation { get; set; }
        public virtual ICollection<KlientFirma> KlientFirma { get; set; }
        public virtual ICollection<PracownikKlient> PracownikKlient { get; set; }
    }
}