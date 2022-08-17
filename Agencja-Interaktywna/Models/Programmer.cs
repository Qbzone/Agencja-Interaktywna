using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Programmer
    {
        public Programmer()
        {
            ProgramistaJezyk = new HashSet<ProgrammerLanguage>();
        }

        public int IdPracownik { get; set; }
        public string PoziomZaawansowania { get; set; }

        public virtual Employee IdPracownikNavigation { get; set; }
        public virtual ICollection<ProgrammerLanguage> ProgramistaJezyk { get; set; }
    }
}