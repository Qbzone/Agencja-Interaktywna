using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Tag
    {
        public Tag()
        {
            FirmaTag = new HashSet<FirmaTag>();
        }

        public int IdTag { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<FirmaTag> FirmaTag { get; set; }
    }
}
