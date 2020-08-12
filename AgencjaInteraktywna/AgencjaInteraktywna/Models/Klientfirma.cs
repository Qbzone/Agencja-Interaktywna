using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna
{
    public partial class Klientfirma
    {
        public int Idklient { get; set; }
        public int Idfirma { get; set; }

        public virtual Firma IdfirmaNavigation { get; set; }
        public virtual Klient IdklientNavigation { get; set; }
    }
}
