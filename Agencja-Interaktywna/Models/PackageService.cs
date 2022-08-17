using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class PackageService
    {
        public int IdPakiet { get; set; }
        public int IdUsluga { get; set; }

        public virtual Package IdPakietNavigation { get; set; }
        public virtual Service IdUslugaNavigation { get; set; }
    }
}