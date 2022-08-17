using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class ProgrammingLanguage
    {
        public ProgrammingLanguage()
        {
            ProgramistaJezyk = new HashSet<ProgrammerLanguage>();
        }

        public int IdJezyk { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<ProgrammerLanguage> ProgramistaJezyk { get; set; }
    }
}