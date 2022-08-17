using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Client
    {
        public Client()
        {
            KlientFirma = new HashSet<ClientCompany>();
            PracownikKlient = new HashSet<EmployeeClient>();
        }

        public int IdKlient { get; set; }
        public string Priorytet { get; set; }

        public virtual Person IdKlientNavigation { get; set; }
        public virtual ICollection<ClientCompany> KlientFirma { get; set; }
        public virtual ICollection<EmployeeClient> PracownikKlient { get; set; }
    }
}