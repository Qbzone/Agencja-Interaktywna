using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Programistajezyk
    {
        public int Idpracownik { get; set; }
        public int Idjezyk { get; set; }
        public int Staz { get; set; }

        public virtual Jezykprogramowania IdjezykNavigation { get; set; }
        public virtual Programista IdpracownikNavigation { get; set; }
    }
}
