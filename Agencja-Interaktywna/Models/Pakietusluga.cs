using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class PakietUsluga
    {
        public int IdPakiet { get; set; }
        public int IdUsluga { get; set; }

        public virtual Pakiet IdPakietNavigation { get; set; }
        public virtual Usluga IdUslugaNavigation { get; set; }
    }
}