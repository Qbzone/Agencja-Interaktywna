using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna
{
    public partial class Firmatag
    {
        public int Idfirma { get; set; }
        public int Idtag { get; set; }

        public virtual Firma IdfirmaNavigation { get; set; }
        public virtual Tag IdtagNavigation { get; set; }
    }
}
