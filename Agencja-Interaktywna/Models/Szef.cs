using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Szef
    {
        public int Idpracownik { get; set; }

        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
