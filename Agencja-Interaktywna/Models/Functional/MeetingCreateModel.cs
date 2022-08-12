using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class MeetingCreateModel
    {
        public PracownikKlient PracownikKlient { get; set; }
        public List<Pracownik> Pracowniks { get; set; }
        public List<Klient> Klients { get; set; }
    }
}