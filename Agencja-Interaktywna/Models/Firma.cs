using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Firma
    {
        public Firma()
        {
            FirmaTag = new HashSet<FirmaTag>();
            KlientFirma = new HashSet<KlientFirma>();
            Projekt = new HashSet<Projekt>();
        }

        public int IdFirma { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<FirmaTag> FirmaTag { get; set; }
        public virtual ICollection<KlientFirma> KlientFirma { get; set; }
        public virtual ICollection<Projekt> Projekt { get; set; }
    }
}
