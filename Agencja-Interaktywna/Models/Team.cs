using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agencja_Interaktywna.Models
{
    public partial class Team
    {
        public Team()
        {
            PracownikZespol = new HashSet<EmployeeTeam>();
            ZespolProjekt = new HashSet<TeamProject>();
        }

        public int? IdZespol { get; set; }
        public string Nazwa { get; set; }
        [NotMapped]
        public string Widok { get; set; }

        public virtual ICollection<EmployeeTeam> PracownikZespol { get; set; }
        public virtual ICollection<TeamProject> ZespolProjekt { get; set; }
    }
}