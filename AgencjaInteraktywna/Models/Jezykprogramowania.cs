using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
{
    public partial class Jezykprogramowania
    {
        public Jezykprogramowania()
        {
            Programistajezyk = new HashSet<Programistajezyk>();
        }

        public int Idjezyk { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<Programistajezyk> Programistajezyk { get; set; }
    }
}
