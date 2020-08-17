using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Pakiet
    {
        public Pakiet()
        {
            Klientpakiet = new HashSet<Klientpakiet>();
            Pakietusluga = new HashSet<Pakietusluga>();
        }

        public int Idpakiet { get; set; }
        public string Nazwa { get; set; }
        public int Oplata { get; set; }
        public string RodzajOplaty { get; set; }

        public virtual ICollection<Klientpakiet> Klientpakiet { get; set; }
        public virtual ICollection<Pakietusluga> Pakietusluga { get; set; }
    }
}
