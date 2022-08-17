using System;
using System.Collections.Generic;

namespace Agencja_Interaktywna.Models
{
    public partial class Contract
    {
        public Contract()
        {
            PracownikUmowa = new HashSet<EmployeeContract>();
        }

        public int IdUmowa { get; set; }
        public string RodzajUmowy { get; set; }

        public virtual ICollection<EmployeeContract> PracownikUmowa { get; set; }
    }
}