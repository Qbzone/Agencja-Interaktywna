using System;
using System.Collections.Generic;

#nullable disable

namespace Agencja_Interaktywna.Models
{
    public partial class Tag
    {
        public Tag()
        {
            FirmaTags = new HashSet<FirmaTag>();
        }

        public int IdTag { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<FirmaTag> FirmaTags { get; set; }
    }
}
