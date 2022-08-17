using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agencja_Interaktywna.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjektPakiet = new HashSet<ProjectPackage>();
            UslugaProjekt = new HashSet<ServiceProject>();
            ZespolProjekt = new HashSet<TeamProject>();
        }

        public int IdProjekt { get; set; }
        [MaxLength(50)]
        public string Nazwa { get; set; }
        public string Logo { get; set; }
        public int? IdFirma { get; set; }
        [NotMapped]
        public string Widok { get; set; }

        public virtual Company IdFirmaNavigation { get; set; }
        public virtual ICollection<ProjectPackage> ProjektPakiet { get; set; }
        public virtual ICollection<ServiceProject> UslugaProjekt { get; set; }
        public virtual ICollection<TeamProject> ZespolProjekt { get; set; }
    }
}