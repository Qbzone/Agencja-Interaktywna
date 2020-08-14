using System;
using System.Collections.Generic;

namespace AgencjaInteraktywna.Models
{
    public partial class Zespol
    {
        public Zespol()
        {
            Pracownikzespol = new HashSet<Pracownikzespol>();
            Zespolprojekt = new HashSet<Zespolprojekt>();
        }

        public int Idzespol { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<Pracownikzespol> Pracownikzespol { get; set; }
        public virtual ICollection<Zespolprojekt> Zespolprojekt { get; set; }
    }
}
