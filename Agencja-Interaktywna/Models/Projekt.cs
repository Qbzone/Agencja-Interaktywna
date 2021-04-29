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
        
        [Required(ErrorMessage = "Proszę podać nazwę projektu.")]
        [MaxLength(50)]
        public string Nazwa { get; set; }
        public string Logo { get; set; }

        [Required(ErrorMessage = "Proszę wybrać firmę.")]
        public int IdFirma { get; set; }
        [NotMapped]
        public string Widok { get; set; }
        [NotMapped]
        [Required(ErrorMessage ="Proszę wybrać logo.")]
        public IFormFile formFile { get; set; }
        public virtual Firma IdFirmaNavigation { get; set; }
        public virtual ICollection<ProjektPakiet> ProjektPakiet { get; set; }
        public virtual ICollection<UslugaProjekt> UslugaProjekt { get; set; }
        public virtual ICollection<ZespolProjekt> ZespolProjekt { get; set; }
    }
}
