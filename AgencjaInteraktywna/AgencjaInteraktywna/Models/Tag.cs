using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna
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
