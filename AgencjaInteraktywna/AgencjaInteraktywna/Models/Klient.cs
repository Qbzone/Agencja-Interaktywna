using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna
{
    public partial class Klient
    {
        public Klient()
        {
            Klientfirma = new HashSet<Klientfirma>();
            Klientpakiet = new HashSet<Klientpakiet>();
            Pracownikklient = new HashSet<Pracownikklient>();
        }

        public int Idklient { get; set; }
        public string Priorytet { get; set; }

        public virtual Osoba IdklientNavigation { get; set; }
        public virtual ICollection<Klientfirma> Klientfirma { get; set; }
        public virtual ICollection<Klientpakiet> Klientpakiet { get; set; }
        public virtual ICollection<Pracownikklient> Pracownikklient { get; set; }
    }
}
