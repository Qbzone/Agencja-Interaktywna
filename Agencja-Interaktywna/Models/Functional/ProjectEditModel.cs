using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class ProjectEditModel
    {
        public Projekt projekt { get; set; }
        public Zespol zespol { get; set; }
        public int IdZespol { get; set; }
        public Pakiet pakiet { get; set; }
        public int IdPakiet { get; set; }
        public List<Firma> firmas { get; set; }
        public List<Zespol> zespols { get; set; }
        public List<Pakiet> pakiets { get; set; }
    }
}
