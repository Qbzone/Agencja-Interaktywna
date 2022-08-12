using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agencja_Interaktywna.Models
{
    public partial class Pakiet
    {
        public Pakiet()
        {
            PakietUsluga = new HashSet<PakietUsluga>();
            ProjektPakiet = new HashSet<ProjektPakiet>();
        }

        public int? IdPakiet { get; set; }
        public string Nazwa { get; set; }
        public int Oplata { get; set; }
        public string RodzajOplaty { get; set; }

        public virtual ICollection<PakietUsluga> PakietUsluga { get; set; }
        public virtual ICollection<ProjektPakiet> ProjektPakiet { get; set; }
    }
}