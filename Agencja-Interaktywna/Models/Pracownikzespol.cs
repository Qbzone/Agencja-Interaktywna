using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class PracownikZespol
    {
        public int IdPracownik { get; set; }
        public int IdZespol { get; set; }
        public DateTime DataPrzypisaniaPracownika { get; set; }
        public DateTime? DataWypisaniaPracownika { get; set; }

        public virtual Pracownik IdPracownikNavigation { get; set; }
        public virtual Zespol IdZespolNavigation { get; set; }
    }
}
