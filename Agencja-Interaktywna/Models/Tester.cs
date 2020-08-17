using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Tester
    {
        public int Idpracownik { get; set; }
        public int Testerdoswiadczenie { get; set; }

        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
