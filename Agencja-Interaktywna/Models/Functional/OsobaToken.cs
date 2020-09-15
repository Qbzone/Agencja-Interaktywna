using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class OsobaToken
    {
        [Display(Name = "Adres E-mail")]
        [Required(ErrorMessage = "Proszę podać swój adres e-mail")]
        public string AdresEmail { get; set; }
    }
}
