using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
{
    public partial class Osoba
    {
        public int Idosoba { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NumerTelefonuPrywatny { get; set; }
        public string NumerTelefonuSłużbowego { get; set; }
        public string AdresEmail { get; set; }
        public string Haslo { get; set; }
        public bool? CzyEmailZweryfikowany { get; set; }
        public Guid? KodAktywacyjny { get; set; }

        public virtual Klient Klient { get; set; }
        public virtual Pracownik Pracownik { get; set; }
    }
}
