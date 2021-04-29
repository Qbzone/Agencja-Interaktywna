using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class ProjectCreateModel
    {
        public Projekt projekt { get; set; }
        public Zespol zespol { get; set; }
        public Pakiet pakiet { get; set; }
        public List<Firma> firmas { get; set; }
        public List<Zespol> zespols { get; set; }
        public List<Pakiet> pakiets { get; set; }
    }
}
