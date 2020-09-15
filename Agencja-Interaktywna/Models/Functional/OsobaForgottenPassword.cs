using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class OsobaForgottenPassword
    {
        [Required]
        public string AdresEmail { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić hasło")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{6,}$", ErrorMessage = "Hasło musi posiadać conajmniej jedną małą literę, jedną dużą literę, jeden znak liczbowy, jeden znak specjalny oraz składać się z co najmniej 6 znaków.")]
        public string Haslo { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Proszę potwierdzić hasło")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{6,}$", ErrorMessage = "Hasło musi posiadać conajmniej jedną małą literę, jedną dużą literę, jeden znak liczbowy, jeden znak specjalny oraz składać się z co najmniej 6 znaków.")]
        [Compare("Haslo", ErrorMessage = "Hasła muszą być takie same")]
        public string PotwierdzHaslo { get; set; }
    }
}
