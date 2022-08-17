using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Graphician
    {
        public int IdPracownik { get; set; }
        public string Specjalizacja { get; set; }

        public virtual Employee IdPracownikNavigation { get; set; }
    }
}