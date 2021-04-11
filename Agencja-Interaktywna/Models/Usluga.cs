using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Usluga
    {
        public Usluga()
        {
            PakietUsluga = new HashSet<PakietUsluga>();
            UslugaProjekt = new HashSet<UslugaProjekt>();
        }

        public int IdUsluga { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<PakietUsluga> PakietUsluga { get; set; }
        public virtual ICollection<UslugaProjekt> UslugaProjekt { get; set; }
    }
}
