using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Programista
    {
        public Programista()
        {
            ProgramistaJezyks = new HashSet<ProgramistaJezyk>();
        }

        public int IdPracownik { get; set; }
        public string PoziomZaawansowania { get; set; }

        public virtual Pracownik IdPracownikNavigation { get; set; }
        public virtual ICollection<ProgramistaJezyk> ProgramistaJezyks { get; set; }
    }
}
