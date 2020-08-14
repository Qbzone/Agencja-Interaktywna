using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
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
