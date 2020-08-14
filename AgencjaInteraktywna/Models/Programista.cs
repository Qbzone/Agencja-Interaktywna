using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
{
    public partial class Programista
    {
        public Programista()
        {
            Programistajezyk = new HashSet<Programistajezyk>();
        }

        public int Idpracownik { get; set; }
        public string Poziomzaawansowania { get; set; }

        public virtual Pracownik IdpracownikNavigation { get; set; }
        public virtual ICollection<Programistajezyk> Programistajezyk { get; set; }
    }
}
