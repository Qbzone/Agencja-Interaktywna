using System;
using System.Collections.Generic;

namespace Interactive_Agency.Models
{
    public partial class Programmer
    {
        public Programmer()
        {
            ProgrammerLanguage = new HashSet<ProgrammerLanguage>();
        }

        public int EmployeeId { get; set; }
        public string AdvancementLevel { get; set; }

        public virtual Employee EmployeeIdkNavigation { get; set; }
        public virtual ICollection<ProgrammerLanguage> ProgrammerLanguage { get; set; }
    }
}