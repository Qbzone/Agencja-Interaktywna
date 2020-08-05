using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencjaInteraktywna.Models
{
    [Table("klient")]
    public partial class Klient
    {
        public Klient()
        {
            Klientfirma = new HashSet<Klientfirma>();
            Klientpakiet = new HashSet<Klientpakiet>();
            Pracownikklient = new HashSet<Pracownikklient>();
        }

        [Key]
        [Column("idklient")]
        public int Idklient { get; set; }
        [Required]
        [Column("priorytet")]
        [StringLength(50)]
        public string Priorytet { get; set; }

        [ForeignKey(nameof(Idklient))]
        [InverseProperty(nameof(Osoba.Klient))]
        public virtual Osoba IdklientNavigation { get; set; }
        [InverseProperty("IdklientNavigation")]
        public virtual ICollection<Klientfirma> Klientfirma { get; set; }
        [InverseProperty("IdklientNavigation")]
        public virtual ICollection<Klientpakiet> Klientpakiet { get; set; }
        [InverseProperty("IdklientNavigation")]
        public virtual ICollection<Pracownikklient> Pracownikklient { get; set; }
    }
}
