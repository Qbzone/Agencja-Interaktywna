using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class TeamProject
    {
        public int IdZespol { get; set; }
        public int IdProjekt { get; set; }
        public DateTime DataPrzypisaniaZespolu { get; set; }
        public DateTime? DataWypisaniaZespolu { get; set; }

        public virtual Project IdProjektNavigation { get; set; }
        public virtual Team IdZespolNavigation { get; set; }
    }
}