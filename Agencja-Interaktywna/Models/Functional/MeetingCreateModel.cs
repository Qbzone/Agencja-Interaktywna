using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class MeetingCreateModel
    {
        public PracownikKlient PracownikKlient { get; set; }
        public List<Pracownik> pracowniks { get; set; }
        public List<Klient> klients { get; set; }
    }
}
