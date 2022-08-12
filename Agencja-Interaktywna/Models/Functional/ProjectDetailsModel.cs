using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class ProjectDetailsModel
    {
        public Projekt projekt { get; set; }
        public List<UslugaProjekt> zadanies { get; set; }

    }
}