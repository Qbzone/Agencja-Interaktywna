using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Zadanie
    {
        public Zadanie()
        {
            ZadanieProjekt = new HashSet<ZadanieProjekt>();
        }

        public int IdZadanie { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<ZadanieProjekt> ZadanieProjekt { get; set; }
    }
}
