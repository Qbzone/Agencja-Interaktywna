using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agencja_Interaktywna.Models.Functional
{
    public class MeetingEditModel
    {
        public EmployeeClient PracownikKlient { get; set; }
        public int IdPracownik { get; set; }
        public int IdKlient { get; set; }
        public DateTime DataRozpoczeciaSpotkania { get; set; }
        public List<Employee> pracowniks { get; set; }
        public List<Client> klients { get; set; }
    }
}