using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna
{
    public partial class Pracownikzespol
    {
        public int Idpracownik { get; set; }
        public int Idzespol { get; set; }
        public DateTime Dataprzypisaniapracownika { get; set; }
        public DateTime? Datawypisaniapracownika { get; set; }
        public bool Menadżer { get; set; }

        public virtual Pracownik IdpracownikNavigation { get; set; }
        public virtual Zespol IdzespolNavigation { get; set; }
    }
}
