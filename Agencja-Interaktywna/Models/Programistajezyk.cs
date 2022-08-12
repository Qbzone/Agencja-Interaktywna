using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class ProgramistaJezyk
    {
        public int IdPracownik { get; set; }
        public int IdJezyk { get; set; }
        public int Staz { get; set; }

        public virtual JezykProgramowania IdJezykNavigation { get; set; }
        public virtual Programista IdPracownikNavigation { get; set; }
    }
}