using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class UslugaProjekt
    {
        public int IdProjekt { get; set; }
        public int IdUsluga { get; set; }
        public DateTime DataPrzypisaniaZadania { get; set; }
        public DateTime? DataZakonczeniaZadania { get; set; }
        public string Status { get; set; }
        public string Opis { get; set; }

        public virtual Projekt IdProjektNavigation { get; set; }
        public virtual Usluga IdUslugaNavigation { get; set; }
    }
}
