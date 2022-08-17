using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agencja_Interaktywna.Models
{
    public partial class EmployeeTeam
    {
        public int IdPracownik { get; set; }
        public int IdZespol { get; set; }
        public DateTime DataPrzypisaniaPracownika { get; set; }
        public DateTime? DataWypisaniaPracownika { get; set; }

        public virtual Employee IdPracownikNavigation { get; set; }
        public virtual Team IdZespolNavigation { get; set; }

    }
}