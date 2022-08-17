using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class ClientCompany
    {
        public int IdKlient { get; set; }
        public int IdFirma { get; set; }

        public virtual Company IdFirmaNavigation { get; set; }
        public virtual Client IdKlientNavigation { get; set; }
    }
}