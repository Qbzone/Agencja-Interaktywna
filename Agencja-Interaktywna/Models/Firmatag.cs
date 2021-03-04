using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class FirmaTag
    {
        public int IdFirma { get; set; }
        public int IdTag { get; set; }

        public virtual Firma IdFirmaNavigation { get; set; }
        public virtual Tag IdTagNavigation { get; set; }
    }
}
