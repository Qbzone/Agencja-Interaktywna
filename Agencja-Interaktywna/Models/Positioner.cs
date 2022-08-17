using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Positioner
    {
        public int IdPracownik { get; set; }
        public string PelnionaFunkcja { get; set; }

        public virtual Employee IdPracownikNavigation { get; set; }
    }
}