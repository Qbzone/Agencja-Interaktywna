using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Service
    {
        public Service()
        {
            PakietUsluga = new HashSet<PackageService>();
            UslugaProjekt = new HashSet<ServiceProject>();
        }

        public int IdUsluga { get; set; }
        public string Nazwa { get; set; }
        public string Klasyfikacja { get; set; }

        public virtual ICollection<PackageService> PakietUsluga { get; set; }
        public virtual ICollection<ServiceProject> UslugaProjekt { get; set; }
    }
}