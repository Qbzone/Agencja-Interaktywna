using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna
{
    public partial class Osoba
    {
        public int Idosoba { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NumerTelefonuPrywatny { get; set; }
        public string NumerTelefonuSłużbowego { get; set; }
        public string AdresEmail { get; set; }

        public virtual Klient Klient { get; set; }
        public virtual Pracownik Pracownik { get; set; }
    }
}
