using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Zadanie
    {
        public Zadanie()
        {
            Zadanieprojekt = new HashSet<Zadanieprojekt>();
        }

        public int Idzadanie { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<Zadanieprojekt> Zadanieprojekt { get; set; }
    }
}
