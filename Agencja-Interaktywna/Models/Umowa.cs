using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Umowa
    {
        public Umowa()
        {
            PracownikUmowas = new HashSet<PracownikUmowa>();
        }

        public int IdUmowa { get; set; }
        public string RodzajUmowy { get; set; }

        public virtual ICollection<PracownikUmowa> PracownikUmowas { get; set; }
    }
}
