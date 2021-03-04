using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class JezykProgramowania
    {
        public JezykProgramowania()
        {
            ProgramistaJezyks = new HashSet<ProgramistaJezyk>();
        }

        public int IdJezyk { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<ProgramistaJezyk> ProgramistaJezyks { get; set; }
    }
}
