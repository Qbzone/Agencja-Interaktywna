using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Projekt
    {
        public Projekt()
        {
            ProjektPakiet = new HashSet<ProjektPakiet>();
            ZadanieProjekt = new HashSet<ZadanieProjekt>();
            ZespolProjekt = new HashSet<ZespolProjekt>();
        }

        public int IdProjekt { get; set; }
        public string Nazwa { get; set; }
        public byte[] Logo { get; set; }
        public int IdFirma { get; set; }

        public virtual Firma IdFirmaNavigation { get; set; }
        public virtual ICollection<ProjektPakiet> ProjektPakiet { get; set; }
        public virtual ICollection<ZadanieProjekt> ZadanieProjekt { get; set; }
        public virtual ICollection<ZespolProjekt> ZespolProjekt { get; set; }
    }
}
