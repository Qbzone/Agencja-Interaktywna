using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class PracownikUmowa
    {
        public int IdPracownik { get; set; }
        public int IdUmowa { get; set; }
        public DateTime DataPodpisaniaUmowy { get; set; }
        public DateTime DataZakonczeniaUmowy { get; set; }

        public virtual Pracownik IdPracownikNavigation { get; set; }
        public virtual Umowa IdUmowaNavigation { get; set; }
    }
}
