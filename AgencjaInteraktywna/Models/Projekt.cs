using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
{
    public partial class Projekt
    {
        public Projekt()
        {
            Zadanieprojekt = new HashSet<Zadanieprojekt>();
            Zespolprojekt = new HashSet<Zespolprojekt>();
        }

        public int Idprojekt { get; set; }
        public string Nazwa { get; set; }
        public int FirmaIdFirma { get; set; }

        public virtual Firma FirmaIdFirmaNavigation { get; set; }
        public virtual ICollection<Zadanieprojekt> Zadanieprojekt { get; set; }
        public virtual ICollection<Zespolprojekt> Zespolprojekt { get; set; }
    }
}
