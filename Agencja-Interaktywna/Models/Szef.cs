using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Szef
    {
        public int IdPracownik { get; set; }

        public virtual Pracownik IdPracownikNavigation { get; set; }
    }
}
