using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class MeetingCreateModel
    {
        public EmployeeClient PracownikKlient { get; set; }
        public List<Employee> Pracowniks { get; set; }
        public List<Client> Klients { get; set; }
    }
}