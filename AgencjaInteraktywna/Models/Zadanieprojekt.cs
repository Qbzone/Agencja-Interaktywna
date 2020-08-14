using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
{
    public partial class Zadanieprojekt
    {
        public int Idzadanie { get; set; }
        public int Idprojekt { get; set; }
        public DateTime Datarozpoczeciazadania { get; set; }
        public DateTime? Datazakonczeniazadania { get; set; }
        public string Status { get; set; }
        public string Opis { get; set; }

        public virtual Projekt IdprojektNavigation { get; set; }
        public virtual Zadanie IdzadanieNavigation { get; set; }
    }
}
