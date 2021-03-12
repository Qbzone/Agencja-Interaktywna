using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class ZadanieProjekt
    {
        public int IdProjekt { get; set; }
        public int IdZadanie { get; set; }
        public DateTime DataPrzypisaniaZadania { get; set; }
        public DateTime? DataZakonczeniaZadania { get; set; }
        public string Status { get; set; }
        public string Opis { get; set; }

        public virtual Projekt IdProjektNavigation { get; set; }
        public virtual Zadanie IdZadanieNavigation { get; set; }
    }
}
