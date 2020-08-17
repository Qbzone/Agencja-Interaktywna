using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Umowa
    {
        public Umowa()
        {
            Pracownikumowa = new HashSet<Pracownikumowa>();
        }

        public int Idumowa { get; set; }
        public string Rodzajumowy { get; set; }

        public virtual ICollection<Pracownikumowa> Pracownikumowa { get; set; }
    }
}
