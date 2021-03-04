using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Zespol
    {
        public Zespol()
        {
            PracownikZespols = new HashSet<PracownikZespol>();
            ZespolProjekts = new HashSet<ZespolProjekt>();
        }

        public int IdZespol { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<PracownikZespol> PracownikZespols { get; set; }
        public virtual ICollection<ZespolProjekt> ZespolProjekts { get; set; }
    }
}
