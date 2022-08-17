using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Boss
    {
        public int IdPracownik { get; set; }

        public virtual Employee IdPracownikNavigation { get; set; }
    }
}