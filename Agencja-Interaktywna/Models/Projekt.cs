using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Projekt
    {
        public Projekt()
        {
            ZadanieProjekts = new HashSet<ZadanieProjekt>();
            ZespolProjekts = new HashSet<ZespolProjekt>();
        }

        public int IdProjekt { get; set; }
        public string Nazwa { get; set; }
        public int IdFirma { get; set; }

        public virtual Firma IdFirmaNavigation { get; set; }
        public virtual ICollection<ZadanieProjekt> ZadanieProjekts { get; set; }
        public virtual ICollection<ZespolProjekt> ZespolProjekts { get; set; }
    }
}
