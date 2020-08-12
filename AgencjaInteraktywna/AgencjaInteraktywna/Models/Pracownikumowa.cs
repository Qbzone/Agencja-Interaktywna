using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna
{
    public partial class Pracownikumowa
    {
        public int Idpracownik { get; set; }
        public int Idumowa { get; set; }
        public DateTime Datapodpisaniaumowy { get; set; }
        public DateTime Datawygasnieciaumowy { get; set; }

        public virtual Pracownik IdpracownikNavigation { get; set; }
        public virtual Umowa IdumowaNavigation { get; set; }
    }
}
