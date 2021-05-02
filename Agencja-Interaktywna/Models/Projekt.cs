using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agencja_Interaktywna.Models
{
    public partial class Projekt 
    {
        public Projekt()
        {
            ProjektPakiet = new HashSet<ProjektPakiet>();
            UslugaProjekt = new HashSet<UslugaProjekt>();
            ZespolProjekt = new HashSet<ZespolProjekt>();
        }

        public int IdProjekt { get; set; }
        [MaxLength(50)]
        public string Nazwa { get; set; }
        public string Logo { get; set; }
        public int? IdFirma { get; set; }
        [NotMapped]
        public string Widok { get; set; }
        
        public virtual Firma IdFirmaNavigation { get; set; }
        public virtual ICollection<ProjektPakiet> ProjektPakiet { get; set; }
        public virtual ICollection<UslugaProjekt> UslugaProjekt { get; set; }
        public virtual ICollection<ZespolProjekt> ZespolProjekt { get; set; }
    }
}
