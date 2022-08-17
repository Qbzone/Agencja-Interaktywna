using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agencja_Interaktywna.Models
{
    public partial class Package
    {
        public Package()
        {
            PakietUsluga = new HashSet<PackageService>();
            ProjektPakiet = new HashSet<ProjectPackage>();
        }

        public int? IdPakiet { get; set; }
        public string Nazwa { get; set; }
        public int Oplata { get; set; }
        public string RodzajOplaty { get; set; }

        public virtual ICollection<PackageService> PakietUsluga { get; set; }
        public virtual ICollection<ProjectPackage> ProjektPakiet { get; set; }
    }
}