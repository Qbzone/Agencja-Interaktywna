﻿using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Firma
    {
        public Firma()
        {
            KlientFirma = new HashSet<KlientFirma>();
            Projekt = new HashSet<Projekt>();
        }

        public int IdFirma { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<KlientFirma> KlientFirma { get; set; }
        public virtual ICollection<Projekt> Projekt { get; set; }
    }
}
