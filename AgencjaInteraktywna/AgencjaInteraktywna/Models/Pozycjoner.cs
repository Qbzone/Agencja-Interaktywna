﻿using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna
{
    public partial class Pozycjoner
    {
        public int Idpracownik { get; set; }
        public string Pelnionafunkcja { get; set; }

        public virtual Pracownik IdpracownikNavigation { get; set; }
    }
}
