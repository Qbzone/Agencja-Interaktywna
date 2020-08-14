using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
{
    public partial class Firma
    {
        public Firma()
        {
            Firmatag = new HashSet<Firmatag>();
            Klientfirma = new HashSet<Klientfirma>();
            Projekt = new HashSet<Projekt>();
        }

        public int Idfirma { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<Firmatag> Firmatag { get; set; }
        public virtual ICollection<Klientfirma> Klientfirma { get; set; }
        public virtual ICollection<Projekt> Projekt { get; set; }
    }
}
