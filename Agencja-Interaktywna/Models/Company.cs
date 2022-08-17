using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Company
    {
        public Company()
        {
            KlientFirma = new HashSet<ClientCompany>();
            Projekt = new HashSet<Project>();
        }

        public int IdFirma { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<ClientCompany> KlientFirma { get; set; }
        public virtual ICollection<Project> Projekt { get; set; }
    }
}