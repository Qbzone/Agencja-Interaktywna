﻿using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
{
    public partial class Klientpakiet
    {
        public int Idklient { get; set; }
        public int Idpakiet { get; set; }
        public DateTime Datarozpoczeciawspolpracy { get; set; }
        public DateTime? Datazakonczeniawspolpracy { get; set; }

        public virtual Klient IdklientNavigation { get; set; }
        public virtual Pakiet IdpakietNavigation { get; set; }
    }
}
