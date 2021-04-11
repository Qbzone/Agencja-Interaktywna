using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class TaskEditModel
    {
        public UslugaProjekt UslugaProjekt { get; set; }
        public List<Usluga> uslugas { get; set; }
    }
}
