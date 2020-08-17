using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Tag
    {
        public Tag()
        {
            Firmatag = new HashSet<Firmatag>();
        }

        public int Idtag { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<Firmatag> Firmatag { get; set; }
    }
}
