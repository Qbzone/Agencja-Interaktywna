using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class ProjektPakiet
    {
        public int IdProjekt { get; set; }
        public int IdPakiet { get; set; }
        public DateTime DataRozpoczeciaWspolpracy { get; set; }
        public DateTime? DataZakonczeniaWspolpracy { get; set; }

        public virtual Pakiet IdPakietNavigation { get; set; }
        public virtual Projekt IdProjektNavigation { get; set; }
    }
}