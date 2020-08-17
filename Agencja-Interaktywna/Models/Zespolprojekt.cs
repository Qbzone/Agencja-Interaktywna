using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Zespolprojekt
    {
        public int Idzespol { get; set; }
        public int Idprojekt { get; set; }
        public DateTime Dataprzypisaniazespolu { get; set; }
        public DateTime? Dataoddaniaprojektu { get; set; }

        public virtual Projekt IdprojektNavigation { get; set; }
        public virtual Zespol IdzespolNavigation { get; set; }
    }
}
