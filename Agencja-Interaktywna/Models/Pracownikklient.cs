using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class PracownikKlient
    {
        public int IdPracownik { get; set; }
        public int IdKlient { get; set; }
        public DateTime DataRozpoczeciaSpotkania { get; set; }
        public DateTime DataZakonczeniaSpotkania { get; set; }
        public string MiejsceSpotkania { get; set; }

        public virtual Klient IdKlientNavigation { get; set; }
        public virtual Pracownik IdPracownikNavigation { get; set; }
    }
}
