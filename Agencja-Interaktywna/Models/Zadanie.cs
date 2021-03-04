using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Zadanie
    {
        public Zadanie()
        {
            ZadanieProjekts = new HashSet<ZadanieProjekt>();
        }

        public int IdZadanie { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<ZadanieProjekt> ZadanieProjekts { get; set; }
    }
}
