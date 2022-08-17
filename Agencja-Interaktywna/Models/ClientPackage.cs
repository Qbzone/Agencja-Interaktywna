using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class ClientPackage
    {
        public int IdKlient { get; set; }
        public int IdPakiet { get; set; }
        public DateTime? DataZakonczeniaWspolpracy { get; set; }
        public DateTime DataRozpoczeciaWspolpracy { get; set; }

        public virtual Client IdKlientNavigation { get; set; }
        public virtual Package IdPakietNavigation { get; set; }
    }
}