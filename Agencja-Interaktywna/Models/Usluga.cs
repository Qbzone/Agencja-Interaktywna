using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Usluga
    {
        public Usluga()
        {
            Pakietusluga = new HashSet<Pakietusluga>();
        }

        public int Idusluga { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }

        public virtual ICollection<Pakietusluga> Pakietusluga { get; set; }
    }
}
