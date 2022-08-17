using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Tester
    {
        public int IdPracownik { get; set; }
        public int TesterDoswiadczenie { get; set; }

        public virtual Employee IdPracownikNavigation { get; set; }
    }
}