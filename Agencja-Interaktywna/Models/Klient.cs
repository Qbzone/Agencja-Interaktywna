using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Klient
    {
        public Klient()
        {
            KlientFirmas = new HashSet<KlientFirma>();
            KlientPakiets = new HashSet<KlientPakiet>();
            PracownikKlients = new HashSet<PracownikKlient>();
        }

        public int IdKlient { get; set; }
        public string Priorytet { get; set; }

        public virtual Osoba IdKlientNavigation { get; set; }
        public virtual ICollection<KlientFirma> KlientFirmas { get; set; }
        public virtual ICollection<KlientPakiet> KlientPakiets { get; set; }
        public virtual ICollection<PracownikKlient> PracownikKlients { get; set; }
    }
}
