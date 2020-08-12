using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna
{
    public partial class Grafik
    {
        public int Idpracownik { get; set; }
        public string Specjalizacja { get; set; }

        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
