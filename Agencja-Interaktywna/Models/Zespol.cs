using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agencja_Interaktywna.Models
{
    public partial class Zespol
    {
        public Zespol()
        {
            PracownikZespol = new HashSet<PracownikZespol>();
            ZespolProjekt = new HashSet<ZespolProjekt>();
        }

        [Required(ErrorMessage = "Proszę wybrać zespół")]
        public int? IdZespol { get; set; }
        public string Nazwa { get; set; }
        [NotMapped]
        public string Widok { get; set; }

        public virtual ICollection<PracownikZespol> PracownikZespol { get; set; }
        public virtual ICollection<ZespolProjekt> ZespolProjekt { get; set; }
    }
}
