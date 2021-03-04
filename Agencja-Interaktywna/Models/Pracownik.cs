using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Pracownik
    {
        public Pracownik()
        {
            PracownikKlients = new HashSet<PracownikKlient>();
            PracownikUmowas = new HashSet<PracownikUmowa>();
            PracownikZespols = new HashSet<PracownikZespol>();
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
        public virtual Programista Programistum { get; set; }
        public virtual Szef Szef { get; set; }
        public virtual Tester Tester { get; set; }
        public virtual ICollection<PracownikKlient> PracownikKlients { get; set; }
        public virtual ICollection<PracownikUmowa> PracownikUmowas { get; set; }
        public virtual ICollection<PracownikZespol> PracownikZespols { get; set; }
    }
}
