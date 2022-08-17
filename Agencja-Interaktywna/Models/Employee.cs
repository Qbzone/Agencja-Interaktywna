using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Employee
    {
        public Employee()
        {
            PracownikKlient = new HashSet<EmployeeClient>();
            PracownikUmowa = new HashSet<EmployeeContract>();
            PracownikZespol = new HashSet<EmployeeTeam>();
        }

        public int IdPracownik { get; set; }
        public string AdresZamieszkania { get; set; }
        public int Pensja { get; set; }
        public int Premia { get; set; }
        public string Pesel { get; set; }
        public int StazPracy { get; set; }

        public virtual Person IdPracownikNavigation { get; set; }
        public virtual Graphician Grafik { get; set; }
        public virtual Positioner Pozycjoner { get; set; }
        public virtual Programmer Programista { get; set; }
        public virtual Boss Szef { get; set; }
        public virtual Tester Tester { get; set; }
        public virtual ICollection<EmployeeClient> PracownikKlient { get; set; }
        public virtual ICollection<EmployeeContract> PracownikUmowa { get; set; }
        public virtual ICollection<EmployeeTeam> PracownikZespol { get; set; }
    }
}