using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
{
    public partial class Pracownik
    {
        public Pracownik()
        {
            Pracownikklient = new HashSet<Pracownikklient>();
            Pracownikumowa = new HashSet<Pracownikumowa>();
            Pracownikzespol = new HashSet<Pracownikzespol>();
        }

        public int Idpracownik { get; set; }
        public string Adreszamieszkania { get; set; }
        public int Pensja { get; set; }
        public int? Premia { get; set; }
        public string Pesel { get; set; }
        public int StazPracy { get; set; }

        public virtual Osoba IdpracownikNavigation { get; set; }
        public virtual Grafik Grafik { get; set; }
        public virtual Pozycjoner Pozycjoner { get; set; }
        public virtual Programista Programista { get; set; }
        public virtual Szef Szef { get; set; }
        public virtual Tester Tester { get; set; }
        public virtual ICollection<Pracownikklient> Pracownikklient { get; set; }
        public virtual ICollection<Pracownikumowa> Pracownikumowa { get; set; }
        public virtual ICollection<Pracownikzespol> Pracownikzespol { get; set; }
    }
}
