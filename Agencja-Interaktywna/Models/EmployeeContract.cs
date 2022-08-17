using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class EmployeeContract
    {
        public int IdPracownik { get; set; }
        public int IdUmowa { get; set; }
        public DateTime DataPodpisaniaUmowy { get; set; }
        public DateTime DataZakonczeniaUmowy { get; set; }

        public virtual Employee IdPracownikNavigation { get; set; }
        public virtual Contract IdUmowaNavigation { get; set; }
    }
}