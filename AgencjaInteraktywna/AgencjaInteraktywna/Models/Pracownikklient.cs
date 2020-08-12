using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna
{
    public partial class Pracownikklient
    {
        public int Idpracownik { get; set; }
        public int Idklient { get; set; }
        public DateTime Datarozpoczeciaspotkania { get; set; }
        public DateTime? Datazakonczeniaspotkania { get; set; }
        public string Miejscespotkania { get; set; }

        public virtual Klient IdklientNavigation { get; set; }
        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
