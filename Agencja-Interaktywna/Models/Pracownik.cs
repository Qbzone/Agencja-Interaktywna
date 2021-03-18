using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Pracownik
    {
        public Pracownik()
        {
            PracownikKlient = new HashSet<PracownikKlient>();
            PracownikUmowa = new HashSet<PracownikUmowa>();
            PracownikZespol = new HashSet<PracownikZespol>();
        }

        public int IdPracownik { get; set; }
        public string AdresZamieszkania { get; set; }
        public int Pensja { get; set; }
        public int Premia { get; set; }
        public string Pesel { get; set; }
        public int StazPracy { get; set; }

        public virtual Osoba IdPracownikNavigation { get; set; }
        public virtual Grafik Grafik { get; set; }
        public virtual Pozycjoner Pozycjoner { get; set; }
        public virtual Programista Programista { get; set; }
        public virtual Szef Szef { get; set; }
        public virtual Tester Tester { get; set; }
        public virtual ICollection<PracownikKlient> PracownikKlient { get; set; }
        public virtual ICollection<PracownikUmowa> PracownikUmowa { get; set; }
        public virtual ICollection<PracownikZespol> PracownikZespol { get; set; }
    }
}
