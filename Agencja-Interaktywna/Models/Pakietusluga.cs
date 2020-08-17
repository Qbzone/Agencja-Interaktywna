using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Pakietusluga
    {
        public int Idpakiet { get; set; }
        public int Idusluga { get; set; }

        public virtual Pakiet IdpakietNavigation { get; set; }
        public virtual Usluga IduslugaNavigation { get; set; }
    }
}
