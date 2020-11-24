using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agencja_Interaktywna.Models
{
    public partial class Osoba
    {

        public int Idosoba { get; set; }
        [Required(ErrorMessage = "Proszę podać swoje imię")]
        [MaxLength(25)]
        public string Imie { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Proszę podać swoje nazwisko")]
        public string Nazwisko { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "W numerze telefonu mogą występować tylko cyfry")]
        [MinLength(9, ErrorMessage = "Numer telefonu może posiadać maks 9 znaków")]
        [MaxLength(9)]
        public string NumerTelefonuPrywatny { get; set; }
        [Required(ErrorMessage = "Proszę podać swój numer telefonu służbowego")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "W numerze telefonu mogą występować tylko cyfry")]
        [MinLength(9, ErrorMessage = "Numer telefonu może posiadać maks 9 znaków")]
        [MaxLength(9)]
        public string NumerTelefonuSluzbowego { get; set; }
        [Required(ErrorMessage = "Proszę podać swój adres e-mail")]
        [MaxLength(50)]
        public string AdresEmail { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić hasło")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{6,}$", ErrorMessage = "Hasło musi posiadać conajmniej jedną małą literę, jedną dużą literę, jeden znak liczbowy, jeden znak specjalny oraz składać się z co najmniej 6 znaków.")]
        public string Haslo { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Proszę potwierdzić hasło")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{6,}$", ErrorMessage = "Hasło musi posiadać conajmniej jedną małą literę, jedną dużą literę, jeden znak liczbowy, jeden znak specjalny oraz składać się z co najmniej 6 znaków.")]
        [Compare("Haslo", ErrorMessage = "Hasła muszą być takie same")]
        public string PotwierdzHaslo { get; set; }
        public bool CzyEmailZweryfikowane { get; set; }
        public Guid KodAktywacyjny { get; set; }
        public string Rola { get; set; }

        public virtual Klient Klient { get; set; }
        public virtual Pracownik Pracownik { get; set; }

    }
}
