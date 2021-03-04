using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Pakiet
    {
        public Pakiet()
        {
            KlientPakiets = new HashSet<KlientPakiet>();
            PakietUslugas = new HashSet<PakietUsluga>();
        }

        public int IdPakiet { get; set; }
        public string Nazwa { get; set; }
        public int Oplata { get; set; }
        public string RodzajOplaty { get; set; }

        public virtual ICollection<KlientPakiet> KlientPakiets { get; set; }
        public virtual ICollection<PakietUsluga> PakietUslugas { get; set; }
    }
}
