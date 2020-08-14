using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
{
    public partial class Szef
    {
        public int Idpracownik { get; set; }

        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
