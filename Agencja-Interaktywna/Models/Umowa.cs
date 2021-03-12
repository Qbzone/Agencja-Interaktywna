using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Umowa
    {
        public Umowa()
        {
            PracownikUmowa = new HashSet<PracownikUmowa>();
        }

        public int IdUmowa { get; set; }
        public string RodzajUmowy { get; set; }

        public virtual ICollection<PracownikUmowa> PracownikUmowa { get; set; }
    }
}
