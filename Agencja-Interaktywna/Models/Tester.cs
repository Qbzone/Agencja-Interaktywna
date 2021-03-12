using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Tester
    {
        public int IdPracownik { get; set; }
        public int TesterDoswiadczenie { get; set; }

        public virtual Pracownik IdPracownikNavigation { get; set; }
    }
}
