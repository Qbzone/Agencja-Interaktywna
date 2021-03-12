using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class JezykProgramowania
    {
        public JezykProgramowania()
        {
            ProgramistaJezyk = new HashSet<ProgramistaJezyk>();
        }

        public int IdJezyk { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<ProgramistaJezyk> ProgramistaJezyk { get; set; }
    }
}
