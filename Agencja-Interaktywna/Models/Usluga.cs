using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Usluga
    {
        public Usluga()
        {
            PakietUslugas = new HashSet<PakietUsluga>();
        }

        public int IdUsluga { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }

        public virtual ICollection<PakietUsluga> PakietUslugas { get; set; }
    }
}
