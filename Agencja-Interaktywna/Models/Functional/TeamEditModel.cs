using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class TeamEditModel
    {
        public Zespol zespol { get; set; }
        public Pracownik pracownik { get; set; }
        public List<CheckBoxItem> pracowniks { get; set; }
    }
}
