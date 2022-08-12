using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class ZespolProjekt
    {
        public int IdZespol { get; set; }
        public int IdProjekt { get; set; }
        public DateTime DataPrzypisaniaZespolu { get; set; }
        public DateTime? DataWypisaniaZespolu { get; set; }

        public virtual Projekt IdProjektNavigation { get; set; }
        public virtual Zespol IdZespolNavigation { get; set; }
    }
}