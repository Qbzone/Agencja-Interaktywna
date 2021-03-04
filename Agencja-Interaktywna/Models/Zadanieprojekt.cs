using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class ZadanieProjekt
    {
        public int IdProjekt { get; set; }
        public int DZadanie { get; set; }
        public DateTime DataPrzypisaniaZadania { get; set; }
        public DateTime? DataZakonczeniaZadania { get; set; }
        public string Status { get; set; }
        public string Opis { get; set; }

        public virtual Zadanie DZadanieNavigation { get; set; }
        public virtual Projekt IdProjektNavigation { get; set; }
    }
}
