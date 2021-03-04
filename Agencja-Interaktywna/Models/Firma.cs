using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Firma
    {
        public Firma()
        {
            FirmaTags = new HashSet<FirmaTag>();
            KlientFirmas = new HashSet<KlientFirma>();
            Projekts = new HashSet<Projekt>();
        }

        public int IdFirma { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<FirmaTag> FirmaTags { get; set; }
        public virtual ICollection<KlientFirma> KlientFirmas { get; set; }
        public virtual ICollection<Projekt> Projekts { get; set; }
    }
}
