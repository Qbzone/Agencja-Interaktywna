﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Pozycjoner
    {
        public int IdPracownik { get; set; }
        public string PelnionaFunkcja { get; set; }

        public virtual Pracownik IdPracownikNavigation { get; set; }
    }
}
