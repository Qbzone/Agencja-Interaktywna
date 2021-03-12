using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class KlientFirma
    {
        public int IdKlient { get; set; }
        public int IdFirma { get; set; }

        public virtual Firma IdFirmaNavigation { get; set; }
        public virtual Klient IdKlientNavigation { get; set; }
    }
}
